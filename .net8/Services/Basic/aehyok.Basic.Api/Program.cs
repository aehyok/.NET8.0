using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using aehyok.Swagger;
using aehyok.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen("aehyok-basic","基础服务");

//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo { Title = "基础服务", Version = "v1" });

//    // 加载程序运行目录下的所有 xml 注释文档
//    Directory.GetFiles(AppContext.BaseDirectory, "*.xml").ToList().ForEach(comment => options.IncludeXmlComments(comment, true));
//});

var app = builder.Build();

app.UseSetStartDefaultRoute();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI( options =>
    //{
    //    options.DocumentTitle = "无代码开发平台";
    //});
    app.UseSwagger("aehyok-basic","基础服务");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
