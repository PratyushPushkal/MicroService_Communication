using Microservice.Service.Nuget;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.service1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("GetbyhttpClinet")]
        public IActionResult Get()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = HttpMethod.Get;
            requestMessage.RequestUri = new Uri(_configuration.GetValue<string>("ServiceUrl")+ "WeatherForecast");

            var resp = httpClient.SendAsync(requestMessage);
            resp.Wait();
            if (resp.Result.IsSuccessStatusCode)
            {
                var content = resp.Result.Content.ReadAsStringAsync();
                content.Wait();
                return Ok(content.Result);
            }
            else
            {
                return null;
            }
        }

        [HttpGet("GetbyNuget")]
        public IActionResult GetbyNuget()
        {
            var result = new WeatherForecastService().GetWeatherForcast();
            return Ok(result);
        }
    }
}
