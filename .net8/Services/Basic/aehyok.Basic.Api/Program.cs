using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using aehyok.Swagger;
using aehyok.Core;

var moduleKey = "aehyok-basic";
var moduleTitle = "��������";

var builder = WebApplication.CreateBuilder(args);
// �жϻ���
builder.Environment.EnvironmentName = "Development";

builder.AddBuilderServices(moduleKey, moduleTitle);

var app = builder.Build();

app.UseApp(moduleKey, moduleTitle);

app.Run();
