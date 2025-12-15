'use client';

import { useState } from 'react';
import { useSignalR } from '@/hooks/useSignalR';
import { NotificationBell } from '@/components/NotificationBell';
import { NotificationPanel } from '@/components/NotificationPanel';
import { ToastNotification } from '@/components/ToastNotification';
import { TestPanel } from '@/components/TestPanel';

export default function Home() {
  const {
    notifications,
    isConnected,
    unreadCount,
    markAsRead,
    markAllAsRead,
    clearNotification,
    clearAllNotifications,
  } = useSignalR();

  const [isPanelOpen, setIsPanelOpen] = useState(false);
  const [latestNotification, setLatestNotification] = useState<typeof notifications[0] | null>(
    null
  );

  // Show toast for new notifications
  useState(() => {
    if (notifications.length > 0 && !notifications[0].read) {
      setLatestNotification(notifications[0]);
    }
  });

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-50 to-gray-100 dark:from-gray-900 dark:to-gray-800">
      {/* Header */}
      <header className="bg-white dark:bg-gray-800 shadow-md">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 flex justify-between items-center">
          <h1 className="text-2xl font-bold text-gray-900 dark:text-white">
            SignalR Notification System
          </h1>
          <NotificationBell
            count={unreadCount}
            onClick={() => setIsPanelOpen(true)}
          />
        </div>
      </header>

      {/* Main Content */}
      <main className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
          {/* Welcome Section */}
          <div className="bg-white dark:bg-gray-800 rounded-lg shadow-md p-8">
            <h2 className="text-3xl font-bold mb-4 text-gray-900 dark:text-white">
              Welcome! 👋
            </h2>
            <p className="text-gray-600 dark:text-gray-300 mb-6">
              This is a real-time notification system built with SignalR and Next.js.
            </p>
            <div className="space-y-4">
              <div className="flex items-start gap-3">
                <div className="flex-shrink-0 w-8 h-8 bg-blue-500 rounded-full flex items-center justify-center text-white">
                  1
                </div>
                <div>
                  <h3 className="font-semibold text-gray-900 dark:text-white">
                    Real-time Updates
                  </h3>
                  <p className="text-sm text-gray-600 dark:text-gray-400">
                    Receive notifications instantly via SignalR
                  </p>
                </div>
              </div>
              <div className="flex items-start gap-3">
                <div className="flex-shrink-0 w-8 h-8 bg-green-500 rounded-full flex items-center justify-center text-white">
                  2
                </div>
                <div>
                  <h3 className="font-semibold text-gray-900 dark:text-white">
                    Toast Notifications
                  </h3>
                  <p className="text-sm text-gray-600 dark:text-gray-400">
                    See notifications pop up in real-time
                  </p>
                </div>
              </div>
              <div className="flex items-start gap-3">
                <div className="flex-shrink-0 w-8 h-8 bg-purple-500 rounded-full flex items-center justify-center text-white">
                  3
                </div>
                <div>
                  <h3 className="font-semibold text-gray-900 dark:text-white">
                    Notification Center
                  </h3>
                  <p className="text-sm text-gray-600 dark:text-gray-400">
                    Manage all your notifications in one place
                  </p>
                </div>
              </div>
            </div>
          </div>

          {/* Test Panel */}
          <TestPanel isConnected={isConnected} />
        </div>

        {/* Stats */}
        <div className="mt-8 grid grid-cols-1 md:grid-cols-3 gap-6">
          <div className="bg-white dark:bg-gray-800 rounded-lg shadow-md p-6">
            <div className="flex items-center justify-between">
              <div>
                <p className="text-sm text-gray-600 dark:text-gray-400">
                  Total Notifications
                </p>
                <p className="text-3xl font-bold text-gray-900 dark:text-white">
                  {notifications.length}
                </p>
              </div>
              <div className="w-12 h-12 bg-blue-100 dark:bg-blue-900 rounded-full flex items-center justify-center">
                <svg
                  className="w-6 h-6 text-blue-600 dark:text-blue-300"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9"
                  />
                </svg>
              </div>
            </div>
          </div>

          <div className="bg-white dark:bg-gray-800 rounded-lg shadow-md p-6">
            <div className="flex items-center justify-between">
              <div>
                <p className="text-sm text-gray-600 dark:text-gray-400">Unread</p>
                <p className="text-3xl font-bold text-gray-900 dark:text-white">
                  {unreadCount}
                </p>
              </div>
              <div className="w-12 h-12 bg-red-100 dark:bg-red-900 rounded-full flex items-center justify-center">
                <svg
                  className="w-6 h-6 text-red-600 dark:text-red-300"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
                  />
                </svg>
              </div>
            </div>
          </div>

          <div className="bg-white dark:bg-gray-800 rounded-lg shadow-md p-6">
            <div className="flex items-center justify-between">
              <div>
                <p className="text-sm text-gray-600 dark:text-gray-400">Status</p>
                <p className="text-3xl font-bold text-gray-900 dark:text-white">
                  {isConnected ? 'Online' : 'Offline'}
                </p>
              </div>
              <div
                className={`w-12 h-12 ${
                  isConnected
                    ? 'bg-green-100 dark:bg-green-900'
                    : 'bg-gray-100 dark:bg-gray-700'
                } rounded-full flex items-center justify-center`}
              >
                <div
                  className={`w-6 h-6 rounded-full ${
                    isConnected ? 'bg-green-500' : 'bg-gray-500'
                  }`}
                />
              </div>
            </div>
          </div>
        </div>
      </main>

      {/* Notification Panel */}
      <NotificationPanel
        notifications={notifications}
        isOpen={isPanelOpen}
        onClose={() => setIsPanelOpen(false)}
        onMarkAsRead={markAsRead}
        onMarkAllAsRead={markAllAsRead}
        onClear={clearNotification}
        onClearAll={clearAllNotifications}
      />

      {/* Toast Notification */}
      {latestNotification && (
        <ToastNotification
          notification={latestNotification}
          onClose={() => setLatestNotification(null)}
        />
      )}
    </div>
  );
}
