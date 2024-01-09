using aehyok.RabbitMQ;
using aehyok.EntityFramework;
using aehyok.Infrastructure.Extensions;
using aehyok.Redis;
using aehyok.Swagger;
using aehyok.Core;

var moduleKey = "aehyok-ncdp";
var moduleTitle = "�޴��뿪��ƽ̨";

var builder = WebApplication.CreateBuilder(args);

// �жϻ���
builder.Environment.EnvironmentName = "Development";

builder.AddBuilderServices(moduleKey, moduleTitle);

var app = builder.Build();

app.UseApp(moduleKey, moduleTitle);

app.Run();
