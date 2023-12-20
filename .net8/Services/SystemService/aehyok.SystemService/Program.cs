// See https://aka.ms/new-console-template for more information
using aehyok.Schedules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using aehyok.RabbitMQ;
using aehyok.EntityFramework;
using Microsoft.Extensions.Hosting;
using aehyok.Swagger;
using aehyok.Core;
using System.Text;

var serviceName = "aehyok-systemservice";

var serviceTitle = "系统服务";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// 判断环境
builder.Environment.EnvironmentName = "Development";
var env = builder.Environment.IsDevelopment;

builder.Host.InitHostAndConfig(serviceName);

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

builder.Services.ConfigureOptions(builder.Configuration);

builder.Services.AddSwaggerGen(serviceName, serviceTitle);

builder.Services.AddEFCoreAndMySql(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddRabbitMQ(builder.Configuration);

builder.Services.AddCronTask();

var app = builder.Build();

app.UseSwagger(serviceName, serviceTitle);

app.AddRabbitMQEventBus();

app.MapGet("/", () => "Hello Sunlight!");
// 运行主机

app.UseSetStartDefaultRoute();

await app.RunAsync();
