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
        _hubContext = hubContext;
    }
    
    //needs to create a onject and sent it using SignR
    public async Task<Notification> SendNotificationAsync(SendNotificationRequest request){ //this fuction sneds a notification
        var notification = new Notification {

            // this makes a new notification
            //using the data that resived form signr
            UserId = request.UserId ?? "all",
            Title = request.Title,
            Message = request.Message,
            Type = request.Type 
        };

        _notifications.Add(notification); //save it 



        //needs tp send it to spesific user 
        //or you can broadcast it to everyone
        if(string.IsNullOrEmpty(request.UserId) || request.UserId == "all" ){
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        } else {
            await _hubContext.Clients.Group(request.UserId).SendAsync("ReceiveNotificcation" ,notification);
        }

        return notification; // give  the notification as a notification
        //if chackes if its sends to all or 
    }

    //needs to mark all the notification as read
    public void MarkAllAsRead(string userId){
        var userNotifications = _notifications.Where(n => n.UserId == userId || n.UserId == "all");
        foreach(var notification in userNotifications){
            notification.isRead = true;
        }
    }

    //get the current number of the unread count 
    public int GetUnreadCount(string UserId){
        return _notifications.Count(n => ! n.isRead && (n.UserId == UserId || n.UserId == "all")
        );

    }

    //get all notifications for a user
    public List<Notification> GetNotifications(string userId){
        return _notifications.Where(n => n.UserId == userId || n.UserId == "all").ToList();
    }

    //mark a specific notification as read
    public Notification? MarkAsRead(string notificationId){
        var notification = _notifications.FirstOrDefault(n => n.Id == notificationId);
        if(notification != null){
            notification.isRead = true;
        }
        return notification;
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
