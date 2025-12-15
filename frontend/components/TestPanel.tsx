'use client';

import React, { useState } from 'react';

interface TestPanelProps {
  isConnected: boolean;
}

export const TestPanel: React.FC<TestPanelProps> = ({ isConnected }) => {
  const [message, setMessage] = useState('');
  const [type, setType] = useState<'info' | 'success' | 'warning' | 'error'>('info');
  const [isSending, setIsSending] = useState(false);

  const sendTestNotification = async () => {
    if (!message.trim()) return;

    setIsSending(true);
    try {
      const response = await fetch(
        process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000/api/notifications/send',
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            message: message.trim(),
            type,
          }),
        }
      );

      if (response.ok) {
        setMessage('');
        console.log('Test notification sent successfully');
      } else {
        console.error('Failed to send test notification');
      }
    } catch (error) {
      console.error('Error sending test notification:', error);
    } finally {
      setIsSending(false);
    }
  };

  return (
    <div className="bg-white dark:bg-gray-800 rounded-lg shadow-md p-6 max-w-md">
      <h3 className="text-lg font-semibold mb-4 text-gray-900 dark:text-white">
        Test Notification Panel
      </h3>
      
      <div className="mb-4">
        <div className="flex items-center gap-2 mb-4">
          <div
            className={`w-3 h-3 rounded-full ${
              isConnected ? 'bg-green-500' : 'bg-red-500'
            }`}
          />
          <span className="text-sm text-gray-600 dark:text-gray-400">
            {isConnected ? 'Connected to SignalR' : 'Disconnected'}
          </span>
        </div>
      </div>

      <div className="space-y-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">
            Message
          </label>
          <input
            type="text"
            value={message}
            onChange={(e) => setMessage(e.target.value)}
            placeholder="Enter notification message"
            className="w-full px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 dark:bg-gray-700 dark:text-white"
            disabled={!isConnected}
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">
            Type
          </label>
          <select
            value={type}
            onChange={(e) =>
              setType(e.target.value as 'info' | 'success' | 'warning' | 'error')
            }
            className="w-full px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 dark:bg-gray-700 dark:text-white"
            disabled={!isConnected}
          >
            <option value="info">Info</option>
            <option value="success">Success</option>
            <option value="warning">Warning</option>
            <option value="error">Error</option>
          </select>
        </div>

        <button
          onClick={sendTestNotification}
          disabled={!isConnected || !message.trim() || isSending}
          className="w-full bg-blue-500 text-white py-2 px-4 rounded-md hover:bg-blue-600 disabled:bg-gray-400 disabled:cursor-not-allowed transition-colors"
        >
          {isSending ? 'Sending...' : 'Send Test Notification'}
        </button>
      </div>
    </div>
  );
};
