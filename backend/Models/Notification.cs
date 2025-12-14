namespace NotificationApi.Models
{
    public class Notification
    {
        public string Id { get; set;} = Guid.NewGuid().ToString(); //Guid.NewGuid() generates a globally unique ID
        public string Userid {get;set;} =string.Empty;
        public string Title {get;set;} =string.Empty;
        public string Massage {get;set;} = string.Empty;

        public string IsReead {get;set;} = fulse;
        public DeateTime CreatedAt {get;set;} =DateTime.UtcNow;

        //add the eneum for the notification type call it here
        //below make it and call it here 

    }
}

public class SendNotificationRequest
{
    public string? USerId{get;set;} 
    public string Title{get;set;} =string.Empty;
    public string Message {get;set;}=string.Empty;
    
    //call the eneum here 
}


//lests mage the eneum

public enum NotificationType{
    info,
    Success,
    Warning,
    error
}

//part 1
// add a table here to store the data 
//date created 
//is it read or not lets make this a bool 
// and the massge body


//part2
//add a eneum to suscess adn warning adn error 

//part 3 
//shuld a massage need a tittle or just a massage and id
// add usir id and masssage then notofication 