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

        public override async Task OnconnectedAsync(){
        //gole is User 123 connected with connection ID: abcXYZ

        }

        public override async Task OnDisconnectedAsync(){
            // do this my implementation when discinected 
        }

        public  async Task MarkAsRead(){

        }

        public async Task MarkAllAsRead(string userId){
            
        }

    }

    
}