'use client';

import { useState, useEffect, useCallback } from 'react';
import * as signalR from '@microsoft/signalr';
import { Notification } from '@/types/notification';

export const useSignalR = () => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [notifications, setNotifications] = useState<Notification[]>([]);
  const [isConnected, setIsConnected] = useState(false);

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(process.env.NEXT_PUBLIC_SIGNALR_HUB_URL || 'http://localhost:5000/notificationHub')
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

          connection.on('ReceiveNotification', (notification: Notification) => {
            setNotifications((prev) => [
              {
                ...notification,
                timestamp: new Date(notification.timestamp),
                read: false,
              },
              ...prev,
            ]);
          });
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
