//this must send notification in real time this has its codes
// and to sore the massages to 
// and treacks read and unread massages form here 

using Microsoft.AspNetCore.SignalR;
using NotificationApi.Hubs;
using NotificationApi.Models;

//imported moderls and hub

namespace NotificationApi.Services{
   public class NotificationService{

    // you need a way to push the massages to the hub
    //and a way to store the mssages 
    //can use static to all the isnstanses share it 
    //need a constricter 
    // need to make a task and send the notification
        //create a storage and store it then Push it cliants 
        //_notifications = store | _hubContext = notify
    // and way to retrive the mssages to
    //and to mark the mssages as read 
    //and mark all as read
    //additionally to get the count of the unread mssages 

    private readonly IHubContext<NotificationHub> _hubContext;
    //made it readonly so no reassigning this when adter made 
    // this is the bridge to the cub without going inside the hub 

    //_notifications is a list of Notification objects.
    //AND MAKE IT A EMPTY one here ~ new()
    private static List<Notification> _notifications = new();

    //make a constroctor
    //A constructor is a special method that runs when you create a new object of the class.
    // and pass a peramter 
    public NotificationService(IHubContext<NotificationHub> hubContext){
        _hubcontext = hubContext;
    }
    



   } 
}



//                  ┌───────────────────────────┐
//                  │  NotificationService      │
//                  │───────────────────────────│
//                  │ Fields:                   │
//                  │ _notifications (List)     │
//                  │ _hubContext (SignalR)     │
//                  └─────────────┬─────────────┘
//                                │
//         ┌──────────────────────┼───────────────────────┐
//         │                      │                       │
//         ▼                      ▼                       ▼
// ┌────────────────┐    ┌────────────────┐       ┌──────────────────┐
// │SendNotification│    │ GetNotifications│       │GetUnreadCount()  │
// │Async(request)  │    │ (userId)       │       │ (userId)         │
// │                │    │                │       │                  │
// │ 1. Create      │    │ 1. Filter      │       │ 1. Count         │
// │    Notification│    │    _notifications│      │    unread        │
// │ 2. Add to      │    │ 2. Return list │       │ 2. Return count  │
// │    _notifications│  │                │       │                  │
// │ 3. Send via    │    │                │       │                  │
// │    _hubContext │    │                │       │                  │
// └────────────────┘    └────────────────┘       └──────────────────┘
//         │
//         │
//         ▼
// ┌───────────────────────────┐
// │MarkAsRead(notificationId) │
// │MarkAllAsRead(userId)      │
// │                           │
// │ 1. Find notifications     │
// │ 2. Set IsRead = true      │
// │ 3. Updates _notifications │
// └───────────────────────────┘
