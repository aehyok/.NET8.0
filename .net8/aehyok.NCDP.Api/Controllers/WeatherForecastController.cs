using aehyok.NCDP;
using aehyok.NCDP.EventData;
using aehyok.NCDP.Services;
using aehyok.RabbitMQ.EventBus;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.NCDP.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IServiceScopeFactory scopeFactory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            this.scopeFactory = scopeFactory;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            using var scope = this.scopeFactory.CreateScope();

            var taskService = scope.ServiceProvider.GetRequiredService<ITaskService>();
            var list = await taskService.GetListAsync();

            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
            }

            var service = scope.ServiceProvider.GetRequiredService<IEventPublisher>();

            service.Publish(new SelfReportPublishEventData()
            {
                TaskId = 111111
            });


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
