// See https://aka.ms/new-console-template for more information
using aehyok.Schedules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using aehyok.RabbitMQ;
using aehyok.RabbitMQ.EventBus;
using aehyok.Schedules.Event;
using aehyok.NCDP.Services;
using aehyok.Infrastructure.Extensions;
using aehyok.EntityFramework;

Console.WriteLine("Hello, World!");

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;

builder.Host.InitHostAndConfig("aehyok.Schedules");

builder.Services.AddEFCoreAndMySql(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddRabbitMQ(builder.Configuration);

builder.Services.AddCronTask();

var app = builder.Build();

app.AddRabbitMQEventBus();

var service = app.Services.GetRequiredService<IEventPublisher>();

service.Publish(new SelfReportPublishEvent()
{
    TaskId = 111111
});

var taskService = app.Services.GetRequiredService<ITaskService>();
var list = await taskService.GetListAsync();

foreach (var item in list)
{
    Console.WriteLine(item.Name);
}
// 运行主机
await app.RunAsync();
