'use client';

import { useState, useEffect, useCallback } from 'react';
import * as signalR from '@microsoft/signalr';
import { Notification } from '@/types/notification';

export const useSignalR = () => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [notifications, setNotifications] = useState<Notification[]>([]);
  const [isConnected, setIsConnected] = useState(false);
  // Use a simple userId for grouping; change this to a real user id when you add auth
  const userId = 'all';
  const hubUrl = process.env.NEXT_PUBLIC_SIGNALR_HUB_URL || 'http://localhost:8080/notificationHub';

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${hubUrl}?userId=${encodeURIComponent(userId)}`)
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          console.log('SignalR Connected!');
          setIsConnected(true);

          // Log incoming notifications so we can verify multi-tab delivery
          connection.on('ReceiveNotification', (notification: Notification) => {
            console.log('ReceiveNotification event received in tab:', { notification });

            setNotifications((prev) => [
              {
                ...notification,
                timestamp: new Date(notification.timestamp),
                read: false,
              },
              ...prev,
            ]);
          });
          // Fetch existing notifications for this user so all tabs start with the same state
          (async () => {
            try {
              const apiBase = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:8080/api/notifications';
              const res = await fetch(`${apiBase}/${encodeURIComponent(userId)}`);
              if (res.ok) {
                const data: any[] = await res.json();
                const mapped = data.map((n) => ({
                  ...n,
                  timestamp: new Date(n.createdAt || n.createdAt || n.createdAt),
                  read: !!n.isRead,
                }));
                setNotifications((prev) => {
                  // merge and dedupe by id
                  const byId = new Map<string, any>();
                  mapped.concat(prev).forEach((item) => byId.set(item.id, item));
                  return Array.from(byId.values()).sort((a, b) => (new Date(b.timestamp).getTime() - new Date(a.timestamp).getTime()));
                });
              } else {
                console.warn('Failed to fetch initial notifications', res.status);
              }
            } catch (err) {
              console.warn('Error fetching initial notifications', err);
            }
          })();
        })
        .catch((error) => console.log('SignalR Connection Error: ', error));
    }

    return () => {
      if (connection) {
        connection.stop();
      }
    };
  }, [connection]);

  const markAsRead = useCallback((id: string) => {
    setNotifications((prev) =>
      prev.map((notif) => (notif.id === id ? { ...notif, read: true } : notif))
    );
  }, []);

  const markAllAsRead = useCallback(() => {
    setNotifications((prev) => prev.map((notif) => ({ ...notif, read: true })));
  }, []);

  const clearNotification = useCallback((id: string) => {
    setNotifications((prev) => prev.filter((notif) => notif.id !== id));
  }, []);

  const clearAllNotifications = useCallback(() => {
    setNotifications([]);
  }, []);

  const unreadCount = notifications.filter((n) => !n.read).length;

  return {
    notifications,
    isConnected,
    unreadCount,
    markAsRead,
    markAllAsRead,
    clearNotification,
    clearAllNotifications,
  };
};
