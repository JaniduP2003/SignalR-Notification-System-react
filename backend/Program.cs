var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:8080");

//configer all thats needed the services
//signalr and add cores
// add cores for the forntned

var app = builder.Build();

//http request pipe line using swagger 
// must use auth to 

app.MapGet("/", () => "Hello World!");

app.Run();
