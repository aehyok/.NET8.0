// See https://aka.ms/new-console-template for more information
using aehyok.Schedules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using aehyok.RabbitMQ;
using aehyok.Infrastructure.Extensions;
using aehyok.EntityFramework;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// 判断环境
var env = builder.Environment.IsDevelopment;

builder.Host.InitHostAndConfig("aehyok.SystemService");

builder.Services.AddEFCoreAndMySql(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddRabbitMQ(builder.Configuration);

builder.Services.AddCronTask();

var app = builder.Build();

app.AddRabbitMQEventBus();
// 运行主机
await app.RunAsync();
