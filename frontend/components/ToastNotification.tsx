'use client';

import React, { useEffect, useState } from 'react';
import { Notification } from '@/types/notification';

interface ToastNotificationProps {
  notification: Notification;
  onClose: () => void;
  duration?: number;
}

export const ToastNotification: React.FC<ToastNotificationProps> = ({
  notification,
  onClose,
  duration = 5000,
}) => {
  const [isVisible, setIsVisible] = useState(false);

  useEffect(() => {
    setIsVisible(true);
    const timer = setTimeout(() => {
      setIsVisible(false);
      setTimeout(onClose, 300);
    }, duration);

    return () => clearTimeout(timer);
  }, [duration, onClose]);

  const getToastColor = (type: Notification['type']) => {
    switch (type) {
      case 'success':
        return 'bg-green-500';
      case 'error':
        return 'bg-red-500';
      case 'warning':
        return 'bg-yellow-500';
      default:
        return 'bg-blue-500';
    }
  };

  return (
    <div
      className={`fixed top-4 right-4 max-w-sm w-full shadow-lg rounded-lg pointer-events-auto ring-1 ring-black ring-opacity-5 overflow-hidden transition-all duration-300 transform z-50 ${
        isVisible ? 'translate-x-0 opacity-100' : 'translate-x-full opacity-0'
      }`}
    >
      <div className={`p-4 ${getToastColor(notification.type)}`}>
        <div className="flex items-start">
          <div className="flex-1">
            <p className="text-sm font-medium text-white">{notification.message}</p>
            <p className="mt-1 text-xs text-white text-opacity-90">
              {new Date(notification.timestamp).toLocaleTimeString()}
            </p>
          </div>
          <button
            onClick={() => {
              setIsVisible(false);
              setTimeout(onClose, 300);
            }}
            className="ml-4 inline-flex text-white hover:text-gray-200 focus:outline-none"
          >
            <svg className="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
              <path
                fillRule="evenodd"
                d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                clipRule="evenodd"
              />
            </svg>
          </button>
        </div>
      </div>
    </div>
  );
};
