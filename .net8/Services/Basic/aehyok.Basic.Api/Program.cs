using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using aehyok.Swagger;
using aehyok.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen("aehyok-basic","��������");

//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo { Title = "��������", Version = "v1" });

//    // ���س�������Ŀ¼�µ����� xml ע���ĵ�
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
    //    options.DocumentTitle = "�޴��뿪��ƽ̨";
    //});
    app.UseSwagger("aehyok-basic","��������");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
