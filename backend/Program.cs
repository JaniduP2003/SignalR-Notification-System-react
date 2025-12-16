using NotificationApi.Hubs;
using NotificationApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:8080");


//configer all thats needed the services
//need to add services to the container 
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Convert enums to strings instead of numbers in JSON
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    }); // this adds the curd code like Httpget in controlers
//below are for swagger so it can see the enepoints
builder.Services.AddEndpointsApiExplorer();
//add swagger
//builder.Services.AddSwaggerGen();


//register the notification service
builder.Services.AddSingleton<NotificationService>();

//signalr and add cores
// add sigenalR for that it need to read the connection string in the appsettings.json
var signalRConnectionString =builder.Configuration["Azure:SignalR:connectionString"];
//lets register this  use the above var 
builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        // Convert enums to strings in SignalR messages too
        options.PayloadSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    })
    .AddAzureSignalR(signalRConnectionString);

// add cores for the forntned
builder.Services.AddCors(options => {
    options.AddPolicy("AllowNextJs",builder =>
    {
        builder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
    });
});

var app = builder.Build();

//http request pipe line using swagger 

if(app.Environment.IsDevelopment()){
    //app.UseSwagger();
    //app.UseSwaggerUI();
}


// must use auth to 
app.UseCors("AllowNextJs");
app.UseHttpsRedirection();  //This forces all requests to switch to HTTPS. maks hhtp to htpps
app.UseAuthorization(); //turns on authatication middleware 

app.MapControllers(); // to detect th ened points 
app.MapHub<NotificationHub>("/notificationHub"); //IMPROTENT
//Create a SignalR endpoint at /notificationHub.” makes https://myserver.com/notificationHub

app.MapGet("/", () => "Hello World!");

app.Run();
