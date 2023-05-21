using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using ActivityService.service;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
ReceiveService receive = new ReceiveService();

builder.Services.AddSingleton(receive);

var app = builder.Build();

app.MapGet("/", () => "hello");

app.Run();