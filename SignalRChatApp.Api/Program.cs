using Microsoft.AspNetCore.Identity;
using SignalRChatApp.Api.Hubs;
using SignalRChatApp.Application;
using SignalRChatApp.Persistence;
using SignalRChatApp.Persistence.DbContext;
using SignalRChatApp.Persistence.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
                      {
                          policy.AllowAnyHeader().
                                 AllowCredentials().
                                 AllowAnyMethod().
                                 SetIsOriginAllowed(origin=> true);
                      });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapHub<ChatHub>("/chat-hub");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
