using SecurityService.service;
using SecurityService.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMessageService, MessageService>();
ReceiveService receive = new ReceiveService();

builder.Services.AddSingleton(receive);

var app = builder.Build();
// hallo
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

DetectionSystem detectionSystem = new DetectionSystem();

detectionSystem.ControllCommand("shadow");

app.Run();

