# SignalR Notification System - Frontend

A real-time notification system built with Next.js 14, TypeScript, and SignalR.

## Project Structure

```
frontend/
├── app/
│   ├── layout.tsx          # Root layout component
│   └── page.tsx            # Main page with notification system
├── components/
│   ├── NotificationBell.tsx    # Bell icon with unread count badge
│   ├── NotificationPanel.tsx   # Sliding panel for notification list
│   ├── ToastNotification.tsx   # Toast popup for new notifications
│   └── TestPanel.tsx           # Testing panel to send notifications
├── hooks/
│   └── useSignalR.ts       # Custom hook for SignalR connection
├── types/
│   └── notification.ts     # TypeScript interfaces
└── .env.local              # Environment variables (not in git)
```

## Features

- 🔔 Real-time notifications via SignalR
- 📱 Toast notifications for instant alerts
- 📊 Notification center with read/unread status
- 🎨 Dark mode support
- ✅ Mark as read functionality
- 🗑️ Clear notifications
- 📈 Live connection status
- 🧪 Built-in test panel

## Getting Started

### Prerequisites

- Node.js 18+ installed
- Backend server running (see backend README)

### Installation

1. Install dependencies:

```bash
npm install
```

2. Create `.env.local` file:

```bash
cp .env.example .env.local
```

3. Update environment variables in `.env.local`:

```env
NEXT_PUBLIC_SIGNALR_HUB_URL=http://localhost:5000/notificationHub
NEXT_PUBLIC_API_URL=http://localhost:5000/api/notifications
```

### Development

Run the development server:

```bash
npm run dev
```

Open [http://localhost:3000](http://localhost:3000) to view the application.

### Build

Build for production:

```bash
npm run build
```

Start production server:

```bash
npm start
```

## Components

### NotificationBell
Bell icon component that displays the unread notification count and opens the notification panel.

### NotificationPanel
Sliding panel that shows all notifications with options to mark as read, clear, and manage notifications.

### ToastNotification
Pop-up toast notification that appears when new notifications arrive.

### TestPanel
Development panel to send test notifications to the system.

## Hooks

### useSignalR
Custom React hook that manages:
- SignalR connection
- Notification state
- Real-time updates
- Connection status

## Types

### Notification Interface
```typescript
interface Notification {
  id: string;
  message: string;
  type: 'info' | 'success' | 'warning' | 'error';
  timestamp: Date;
  read: boolean;
}
```

## Styling

- **Framework**: Tailwind CSS 4
- **Dark Mode**: Automatic system preference detection
- **Icons**: SVG icons
- **Responsive**: Mobile-first design

## Technologies

- **Next.js 16** - React framework
- **TypeScript** - Type safety
- **SignalR** - Real-time communication
- **Tailwind CSS** - Styling
- **React 19** - UI library

## Environment Variables

| Variable | Description | Example |
|----------|-------------|---------|
| `NEXT_PUBLIC_SIGNALR_HUB_URL` | SignalR hub URL | `http://localhost:5000/notificationHub` |
| `NEXT_PUBLIC_API_URL` | Backend API URL | `http://localhost:5000/api/notifications` |

## Learn More

- [Next.js Documentation](https://nextjs.org/docs)
- [SignalR Documentation](https://docs.microsoft.com/en-us/aspnet/core/signalr/)
- [Tailwind CSS](https://tailwindcss.com/docs)
