//Controllers/ → for API controllers
//Hubs/ → for SignalR hubs
// just like that contorller for the SignR

//controlers are mostly REST req and resive
//BUT hubs are REEEALTIME
//signR is FULLY ASYNC

using Microsoft.AspNetCore.SignalR;

namespace NotificationApi.Hubs {

    public class NotificationHub : Hub {
        //:R this means it gets fetures of signalR
        //all must be async 

        public override async Task OnConnectedAsync(){
        //gole is User 123 connected with connection ID: abcXYZ
            var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();
            //Request.Query["userId"] reads form url  /notificationHub?userId=123
            //now chack of the user id is valid 
            if(!string.IsNullOrEmpty(userId)){
                await Groups.AddToGroupAsync(Context.ConnectionId , userId);
                //groups A built-in SignalR group manager CANT do Add connections to groups Remove connections from groups
                Console.WriteLine($"User{userId} connceted with connection it of :{Context.ConnectionId}");
            }

            await base.OnConnectedAsync();
            //tells "After my custom connection logic, let SignalR run its own default connection logic."
            //(await)Waits for the base method to finish (base)Refers to the parent class


        }

        public override async Task OnDisconnectedAsync(Exception? exception){
            // do this my implementation when discinected 
            var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
                Console.WriteLine($"User {userId} disconnected");
            }
            
            await base.OnDisconnectedAsync(exception);

        }

        public  async Task MarkAsRead(string userId, string notificationId){
            // need to say Send a real-time message to all connected devices of a specific 
            // user saying "this notification was read".
            //"NotificationMarkedAsRead" Event name (string)
            await Clients.Group(userId).SendAsync("NotificationMarkedAsRead",notificationId);
            //This line pushes a real-time "notification read" event to every active device of a specific user.

        }

        public async Task MarkAllAsRead(string userId){
        //This line broadcasts a "clear all notifications" event to every active device of a specific user in real time.
        await Clients.Group(userId).SendAsync("AllNofificetionsMarkedAsRead");

        }

    }

    
}
