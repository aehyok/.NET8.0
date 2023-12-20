using aehyok.RabbitMQ;
using aehyok.EntityFramework;
using aehyok.Infrastructure.Extensions;
using aehyok.Redis;
using aehyok.Swagger;
using aehyok.Core;

var moduleKey = "aehyok-ncdp";
var moduleTitle = "无代码开放平台";

var builder = WebApplication.CreateBuilder(args);

// 判断环境
builder.Environment.EnvironmentName = "Development";

builder.AddBuilderServices(moduleKey, moduleTitle);

var app = builder.Build();

app.UseApp(moduleKey, moduleTitle);

app.Run();
