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





    }

 }