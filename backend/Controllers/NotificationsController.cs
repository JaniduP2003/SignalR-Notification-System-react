 using Microsoft.AspNetCore.Mvc;
 using NotificationApi.Models;
 using NotificationApi.Services;

 namespace NotificationApi.Controllers{

    //this class only for oruting so add apicontroler keyworf
    [ApiController]
    [Router("api/[ccontroller]")]

    public class NotificationsController:ControllerBase{
        //“This controller uses NotificationService to do all notification work.”
        private readonly NotificationService _notificationService;
        
        
        //make the constractor
        //NotificationService (Capital N, S) → TYPE / CLASS name 
        // notificationService (small n, s) → VARIABLE / PARAMETER name
        public NotificationsController(NotificationService notificationService){
            // this is run whwen .asp is run
            _notificationService = notificationService;
        }

        [httpPost("send")]
        public async Task<IActionResult> SendNotification([Frombody] SendNotificationRequest request){
            //formbody mean The data I need is in the BODY of the HTTP request"
            //sendnotificationreq is the json object i made in servises 
            //request is the variable it is put on
            var notification = await _notificationService.SendNotificationAsync(request);
            return Ok(notification);
        }

        //get all the mssages of the user 
        //create a methode called GETNOTIFICAtiONs
        //get the userid
        [HttpGet("{userId}")]
        public IActionResult GetNotifications(string userId){
            var notifications = _notificationService.GetNotifications(userId);
            return Ok(notifications);
        }

        [HttpPost("{notificationId}/read")]
        public IActionResult MarkAsRead(string notificationId){
            var notification =_notificationService.GetNotifications(userId);
            return Ok(notification);
        }

        [HttpPost("{userId}/read-all")]
        public IActionResult MarkAllAsRead(string userId)
        {
            _notificationService.MarkAllAsRead(userId);
            return Ok();
        }

        [HttpGet("{userId}/unread-count")]
        public IActionResult GetUnreeadCount(string userId){
            var count =_notificationService.GetUnreadCount(user);
            //“Ask the NotificationService to count how many unread notifications this user has.”
            //_notificationService → the object that contains all the notification logic
            return Ok(new {count});
        }

    





    }

 }