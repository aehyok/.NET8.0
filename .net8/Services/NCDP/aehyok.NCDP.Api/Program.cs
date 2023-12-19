using aehyok.RabbitMQ;
using aehyok.EntityFramework;
using aehyok.Infrastructure.Extensions;
using aehyok.Redis;
using aehyok.Swagger;
using aehyok.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen("aehyok-ncdp", "无代码开放平台");

builder.Host.InitHostAndConfig("aehyok-ncdp");

builder.Services.AddEFCoreAndMySql(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddRabbitMQ(builder.Configuration);

var app = builder.Build();


app.UseSetStartDefaultRoute();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger("aehyok-ncdp", "无代码开放平台");
}

app.AddRedis(app.Configuration);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
