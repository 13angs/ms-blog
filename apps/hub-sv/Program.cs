using Api.Common.Interfaces;
using Api.Common.Services;
using hub_sv.Hubs;
using Newtonsoft.Json.Serialization;
using Simple.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);
IConfiguration _configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessageSubscriber, MessageSubscriber>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IRealtime, RealtimeService>();

// configure controller to use Newtonsoft as a default serializer
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
            .Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver=new DefaultContractResolver();
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); 
app.UseRouting();

app.MapHub<BlogHub>(_configuration["SignalrConfig:Endpoints:Blog"]);

app.UseAuthorization();

app.MapControllers();

app.Run();
