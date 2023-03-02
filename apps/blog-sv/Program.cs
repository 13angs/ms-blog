using Api.Common.exceptions;
using blog_sv.Interfaces;
using blog_sv.Services;
using Newtonsoft.Json.Serialization;
using Simple.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBlogDbContext, BlogDbContext>();
builder.Services.AddScoped<IBlog, BlogService>();
builder.Services.AddScoped<IMessagePublisher, MessagePublisher>();

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
app.UseResponseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
