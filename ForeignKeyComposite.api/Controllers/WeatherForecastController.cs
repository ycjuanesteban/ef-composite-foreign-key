using ForeignKeyComposite.api.Data;
using ForeignKeyComposite.api.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ForeignKeyComposite.api.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            AddToBBDD();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        private void AddToBBDD()
        {
            FKCContext fKCContext = new FKCContext();

            Entity01 entity01 = new Entity01()
            {
                Key01 = 1,
                Key02 = 1,
                Entities02 = new List<Entity02>()
                {
                    new Entity02() { Foreign01 = 1, Foreign02 = 1, OtherProperty = "OtherProperty" },
                    new Entity02() { Foreign01 = 1, Foreign02 = 1, OtherProperty = "OtherProperty" },
                    new Entity02() { Foreign01 = 1, Foreign02 = 1, OtherProperty = "OtherProperty" },
                }
            };

            fKCContext.Entity01.Add(entity01);
            fKCContext.SaveChanges();

            Entity01 entity0102 = new Entity01()
            {
                Key01 = 1,
                Key02 = 2,
            };

            fKCContext.Entity01.Add(entity0102);
            fKCContext.SaveChanges();

            var foundEntity0102 = fKCContext.Entity01.Where(x => x.Key01 == 1 && x.Key02 == 2).First();

            for (int i = 0; i < 20; i++)
            {
                fKCContext.Entity02.Add(new Entity02()
                {
                    Foreign01 = foundEntity0102.Key01,
                    Foreign02= foundEntity0102.Key02,
                    OtherProperty = $"{i}-OtherProperty"
                });
            }
            fKCContext.SaveChanges();

        }
    }
}