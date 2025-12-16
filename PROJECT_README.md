# 🔔 SignalR Notification System

A full-stack real-time notification system demonstrating SignalR integration with Next.js and ASP.NET Core.

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Technology Stack](#technology-stack)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [API Documentation](#api-documentation)
- [SignalR Events](#signalr-events)
- [Frontend Implementation](#frontend-implementation)
- [Backend Implementation](#backend-implementation)
- [Testing](#testing)
- [Troubleshooting](#troubleshooting)
- [Future Enhancements](#future-enhancements)

---

## 🎯 Overview

This project demonstrates a production-ready pattern for implementing real-time notifications using SignalR with Azure SignalR Service support. It showcases:

- Real-time push notifications across multiple clients
- Multi-tab synchronization
- REST API + WebSocket hybrid architecture
- In-memory notification management with broadcast capabilities

**Live Demo Flow:**
1. User creates notification via Test Panel
2. Backend stores notification and broadcasts via SignalR
3. All connected clients receive the notification in real-time
4. Toast notification appears, counters update
5. Actions (mark read, clear) sync across all tabs

---

## ✨ Features

### Core Features
- ✅ **Real-time Notifications** - Instant delivery via SignalR
- ✅ **Multi-tab Sync** - Actions in one tab reflect in all tabs
- ✅ **Toast Notifications** - Non-intrusive popup alerts
- ✅ **Notification Center** - Centralized management panel
- ✅ **Read/Unread Tracking** - Mark individual or all as read
- ✅ **Clear Notifications** - Remove single or all notifications
- ✅ **Type Categorization** - Info, Success, Warning, Error types
- ✅ **Azure SignalR Ready** - Scalable cloud deployment

### UI Features
- Notification bell with badge counter
- Sliding notification panel
- Test panel for creating notifications
- Real-time status indicator
- Dark mode support

---

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                        Frontend (Next.js)                    │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │ Test Panel   │  │ Notification │  │   Toast      │      │
│  │ (Send)       │  │ Panel        │  │ Notification │      │
│  └──────┬───────┘  └──────┬───────┘  └──────┬───────┘      │
│         │                  │                  │              │
│         └──────────────────┼──────────────────┘              │
│                            │                                 │
│                    ┌───────▼────────┐                        │
│                    │  useSignalR    │                        │
│                    │  Hook          │                        │
│                    └───────┬────────┘                        │
│                            │                                 │
└────────────────────────────┼─────────────────────────────────┘
                             │
                ┌────────────┼────────────┐
                │            │            │
         REST API         SignalR      WebSocket
                │            │            │
                ▼            ▼            ▼
┌─────────────────────────────────────────────────────────────┐
│                    Backend (ASP.NET Core)                    │
│                                                               │
│  ┌──────────────────┐      ┌──────────────────┐            │
│  │  Controllers/    │      │  Hubs/           │            │
│  │  Notifications   │──────│  NotificationHub │            │
│  │  Controller      │      │                  │            │
│  └────────┬─────────┘      └────────┬─────────┘            │
│           │                         │                       │
│           └─────────┬───────────────┘                       │
│                     │                                       │
│           ┌─────────▼──────────┐                           │
│           │  Services/         │                           │
│           │  Notification      │                           │
│           │  Service           │                           │
│           └─────────┬──────────┘                           │
│                     │                                       │
│           ┌─────────▼──────────┐                           │
│           │  In-Memory Storage │                           │
│           │  (List<Notification>)                          │
│           └────────────────────┘                           │
│                                                               │
│           ┌────────────────────┐                           │
│           │ Azure SignalR      │ (Optional)                │
│           │ Service            │                           │
│           └────────────────────┘                           │
└─────────────────────────────────────────────────────────────┘
```

### Communication Flow

**Creating a Notification:**
```
Frontend → POST /api/notifications/send → Controller → Service
→ Store in memory → Broadcast via SignalR → All Clients Update
```

**Marking as Read:**
```
Frontend → POST /api/notifications/{id}/read → Controller → Service
→ Update in memory → Broadcast "NotificationMarkedAsRead" → All Clients Update
```

**Clearing Notification:**
```
Frontend → DELETE /api/notifications/{id} → Controller → Service
→ Remove from memory → Broadcast "NotificationCleared" → All Clients Update
```

---

## 🛠️ Technology Stack

### Frontend
- **Framework:** Next.js 16.0.10
- **Language:** TypeScript
- **UI:** React + Tailwind CSS
- **SignalR Client:** @microsoft/signalr
- **Build Tool:** Turbopack

### Backend
- **Framework:** ASP.NET Core (net9.0)
- **Language:** C#
- **SignalR:** Microsoft.AspNetCore.SignalR
- **Azure SignalR:** Microsoft.Azure.SignalR (optional)
- **CORS:** Configured for localhost:3000

### Infrastructure
- **Dev Server:** http://localhost:8080 (backend)
- **Dev Server:** http://localhost:3000 (frontend)
- **SignalR Hub:** /notificationHub
- **Azure SignalR:** Optional cloud scaling

---

## 🚀 Getting Started

### Prerequisites

- **Node.js** 18+ (with npm or yarn)
- **.NET SDK** 9.0+ (or .NET 7/8)
- **Git** (for cloning)
- **Code Editor** (VS Code recommended)

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/JaniduP2003/SignalR-Notification-System-react.git
cd SignalR-Notification-System-react
```

2. **Start the Backend**
```bash
cd backend
dotnet restore
dotnet run
```

Expected output:
```
Now listening on: http://localhost:8080
Service connection connected to Azure SignalR (if configured)
```

3. **Start the Frontend** (in a new terminal)
```bash
cd frontend
npm install
npm run dev
```

Expected output:
```
▲ Next.js 16.0.10
- Local:        http://localhost:3000
```

4. **Open Browser**
Navigate to: http://localhost:3000

---

## ⚙️ Configuration

### Backend Configuration

**File:** `backend/appsettings.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Azure": {
    "SignalR": {
      "ConnectionString": "YOUR_AZURE_SIGNALR_CONNECTION_STRING"
    }
  }
}
```

**To use Azure SignalR Service:**
1. Create an Azure SignalR Service resource
2. Copy the connection string
3. Paste into `appsettings.json`
4. Restart the backend

**Without Azure SignalR:**
- Leave the connection string empty
- SignalR will use local WebSocket connections

### Frontend Configuration

**File:** `frontend/.env.local`

```env
NEXT_PUBLIC_SIGNALR_HUB_URL=http://localhost:8080/notificationHub
NEXT_PUBLIC_API_URL=http://localhost:8080/api/notifications
```

**Important:** Restart the frontend after changing `.env.local`

---

## 📡 API Documentation

### Base URL
`http://localhost:8080/api/notifications`

### Endpoints

#### 1. Send Notification
```http
POST /send
Content-Type: application/json

{
  "request": {
    "message": "Your notification message",
    "title": "Optional Title",
    "type": "info",
    "userId": "all"
  }
}
```

**Type values:** `info`, `Success`, `Warning`, `error` (case-sensitive)

**Response:**
```json
{
  "id": "guid-string",
  "userId": "all",
  "title": "Optional Title",
  "message": "Your notification message",
  "isRead": false,
  "createdAt": "2025-12-16T10:30:00Z",
  "type": "info"
}
```

#### 2. Get Notifications
```http
GET /{userId}
```

Returns array of notifications visible to the user.

#### 3. Mark as Read
```http
POST /{notificationId}/read
```

Marks notification as read and broadcasts event.

#### 4. Mark All as Read
```http
POST /{userId}/read-all
```

Marks all user notifications as read and broadcasts event.

#### 5. Delete Notification
```http
DELETE /{notificationId}
```

Removes notification and broadcasts event.

#### 6. Clear All Notifications
```http
DELETE /{userId}/clear-all
```

Removes all user notifications and broadcasts event.

#### 7. Get Unread Count
```http
GET /{userId}/unread-count
```

**Response:**
```json
{
  "count": 5
}
```

### cURL Examples

**Send a notification:**
```bash
curl -X POST http://localhost:8080/api/notifications/send \
  -H "Content-Type: application/json" \
  -d '{
    "request": {
      "message": "Test from cURL",
      "type": "Success",
      "title": "API Test",
      "userId": "all"
    }
  }'
```

**Get notifications:**
```bash
curl http://localhost:8080/api/notifications/all
```

**Mark as read:**
```bash
curl -X POST http://localhost:8080/api/notifications/{notification-id}/read
```

**Delete notification:**
```bash
curl -X DELETE http://localhost:8080/api/notifications/{notification-id}
```

---

## 🔌 SignalR Events

### Client Connection
```
ws://localhost:8080/notificationHub?userId={userId}
```

The `userId` query parameter is used for grouping. All connections with the same `userId` receive the same broadcasts.

### Server → Client Events

| Event Name | Payload | Description |
|------------|---------|-------------|
| `ReceiveNotification` | `Notification` object | New notification created |
| `NotificationMarkedAsRead` | `notificationId` (string) | Notification marked as read |
| `AllNofificetionsMarkedAsRead` | none | All notifications marked as read |
| `NotificationCleared` | `notificationId` (string) | Notification deleted |
| `AllNotificationsCleared` | none | All notifications deleted |

### Event Examples

**ReceiveNotification:**
```javascript
connection.on('ReceiveNotification', (notification) => {
  console.log('New notification:', notification);
  // Update UI
});
```

**NotificationMarkedAsRead:**
```javascript
connection.on('NotificationMarkedAsRead', (notificationId) => {
  console.log('Marked as read:', notificationId);
  // Update UI
});
```

---

## 💻 Frontend Implementation

### Key Files

- `hooks/useSignalR.ts` - SignalR connection and state management
- `components/TestPanel.tsx` - Test notification creation UI
- `components/NotificationPanel.tsx` - Notification list and management
- `components/NotificationBell.tsx` - Bell icon with badge
- `components/ToastNotification.tsx` - Popup notification
- `app/page.tsx` - Main page component

### useSignalR Hook

```typescript
const {
  notifications,      // Array of notifications
  isConnected,        // SignalR connection status
  unreadCount,        // Count of unread notifications
  markAsRead,         // Function to mark one as read
  markAllAsRead,      // Function to mark all as read
  clearNotification,  // Function to clear one
  clearAllNotifications, // Function to clear all
} = useSignalR();
```

### Connection Lifecycle

1. **Mount:** Create SignalR connection
2. **Connect:** Start connection and subscribe to events
3. **Fetch:** Load existing notifications from API
4. **Listen:** Receive real-time events
5. **Unmount:** Stop connection

---

## 🔧 Backend Implementation

### Key Files

- `Program.cs` - App configuration and startup
- `Controllers/NotificationsController.cs` - REST API endpoints
- `Services/NotificationService.cs` - Business logic and broadcasting
- `Hubs/NotificationHub.cs` - SignalR hub
- `Models/Notification.cs` - Data models

### NotificationService

The service manages:
- In-memory storage (`List<Notification>`)
- CRUD operations
- SignalR broadcasting to groups/all clients

### NotificationHub

Manages:
- Connection lifecycle
- User group assignment
- Connection tracking

---

## 🧪 Testing

### Manual Testing

1. **Single Tab Test**
   - Open http://localhost:3000
   - Create a notification
   - Verify toast appears
   - Verify counters update
   - Mark as read, verify UI updates

2. **Multi-Tab Test**
   - Open two tabs to http://localhost:3000
   - Send notification from Tab A
   - Verify Tab B receives it
   - Mark as read in Tab A
   - Verify Tab B updates
   - Clear in Tab B
   - Verify Tab A updates

3. **DevTools Inspection**
   - Open DevTools → Console
   - Look for SignalR connection logs
   - Look for event logs
   - Open Network tab → WS
   - Inspect WebSocket frames

### Expected Console Logs

```
SignalR Connected!
ReceiveNotification event received in tab: { notification: {...} }
NotificationMarkedAsRead event: abc-123
NotificationCleared event received: abc-123
```

---

## 🐛 Troubleshooting

### Frontend shows "Offline"

**Cause:** SignalR connection failed

**Fix:**
1. Check backend is running on port 8080
2. Verify `NEXT_PUBLIC_SIGNALR_HUB_URL` in `.env.local`
3. Check browser console for errors
4. Verify CORS settings in `Program.cs`

### POST /send returns 400

**Cause:** Invalid request payload

**Fix:**
1. Ensure JSON uses `{ request: { ... } }` wrapper
2. Check `type` enum casing: `info`, `Success`, `Warning`, `error`
3. Verify Content-Type header is `application/json`

### Tabs don't sync

**Cause:** Different `userId` or missing events

**Fix:**
1. Verify both tabs use same `userId` query parameter
2. Check console for event logs in both tabs
3. Verify backend broadcasts are executing
4. Check WebSocket connection is active

### Azure SignalR connection fails

**Cause:** Invalid connection string or configuration

**Fix:**
1. Verify connection string format
2. Check Azure SignalR service is running
3. Verify service tier supports WebSocket
4. Check firewall/network settings

---

## 🔮 Future Enhancements

### Planned Features
- [ ] Persistent storage (database)
- [ ] User authentication (JWT)
- [ ] Notification templates
- [ ] Push notifications (web push)
- [ ] Email notifications
- [ ] Notification scheduling
- [ ] Read receipts
- [ ] Notification history
- [ ] Search and filter
- [ ] Notification preferences
- [ ] Mobile responsive improvements
- [ ] Unit and integration tests

### Architecture Improvements
- [ ] Add Redis for distributed caching
- [ ] Implement CQRS pattern
- [ ] Add message queue (RabbitMQ/Azure Service Bus)
- [ ] Implement retry logic
- [ ] Add health checks
- [ ] Monitoring and logging (Application Insights)
- [ ] Rate limiting
- [ ] API versioning

---

## 📄 License

This project is provided as-is for educational and demonstration purposes.

## 👤 Author

**Janidu Punchihewa**
- GitHub: [@JaniduP2003](https://github.com/JaniduP2003)

## 🙏 Acknowledgments

- Microsoft SignalR Team
- Next.js Team
- Azure Team

---

**For questions or issues, please open a GitHub issue.**
