// See https://aka.ms/new-console-template for more information
using aehyok.Infrastructure;
using aehyok.Schedules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using aehyok.RabbitMQ;
using aehyok.RabbitMQ.EventBus;
using aehyok.Schedules.Event;
using System.Runtime.Loader;
using aehyok.NCDP.Services;
using aehyok.Infrastructure.Extensions;
Console.WriteLine("Hello, World!");

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;

// 例如 aehyok.NCDP 最开始代码中没有使用到，是不会加载到内存中的，所以需要手动加载
Directory.GetFiles(AppContext.BaseDirectory, "aehyok.*.dll").Select(AssemblyLoadContext.Default.LoadFromAssemblyPath).ToList();

Thread.CurrentThread.Name = "aehyok.Schedules";
builder.Host.InitHost("aehyok.Schedules");

builder.Services.AddMysql(builder.Configuration);

builder.Services.AddServices();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddRabbitMQ(builder.Configuration);

builder.Services.AddCronServices();

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
