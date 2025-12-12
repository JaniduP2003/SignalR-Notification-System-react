using NotificationApi.Hubs;
using NotificationApi.services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:8080");


//configer all thats needed the services
//need to add services to the container 
builder.Service.AddControllers(); // this adds the curd code like Httpget in controlers
//below are for swagger so it can see the enepoints
builder.services.AddEndpointsApiExplorer();
//add swagger
builder.services.swagger();


//signalr and add cores
// add sigenalR for that it need to read the connection string in the appsettings.json
var signalRConnectionString =builder.Configuration["Azure:SignalR:connectionString"];
//lets register this  use the above var 
builder.services.AddSignalR().AddAzureSignalR(signalRConnectionString);

// add cores for the forntned
builder.services.AddCors(options => {
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
    app.UseSwagger;
    app.UseSwaggerUI;
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
