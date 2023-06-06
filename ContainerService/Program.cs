using ContainerService.data;
using Microsoft.EntityFrameworkCore;
using ContainerService.services;
using ContainerService.models;
using MongoDB.Driver;
using System.Xml.Serialization;
using ContainerService.service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader())
);


builder.Services.AddSingleton<IMessageService, MessageService>();
ReceiveService receive = new ReceiveService();

builder.Services.AddSingleton(receive);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.Configure<MongoDBConfig>(builder.Configuration.GetSection("MongoDBConfig"));
builder.Services.AddTransient<ICommandService, CommandService>();
builder.Services.AddTransient<CommandApp>();

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var connString = builder.Configuration.GetConnectionString("MongoConnection");
    return new MongoClient(connString);
});

builder.Services.AddScoped<CommandContext>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoConnection");
    var databaseName = "command_mongodb"; // Replace with your desired database name

    var mongoClient = serviceProvider.GetService<IMongoClient>();
    var mongoDatabase = mongoClient.GetDatabase(databaseName);

    return new CommandContext(connectionString, databaseName);
});

var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var initialize = serviceProvider.GetService<CommandService>();
    var commandservice = serviceProvider.GetService<ICommandService>();
    initialize?.Initialize(); // Add a method in ReportRepository to handle initialization if required
}

app.Run();

