using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly ILoggerManager _logger;

        public WeatherForecastController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {

            _logger.logInfo("Here is info message from our values controller.");
            _logger.logDebug("Here is debug message from our values controller.");
            _logger.logWarning("Here is warn message from our values controller.");
            _logger.logError("Here is an error message from our values controller.");

            return new string[] { "value1", "value2" };

        }
    }
}