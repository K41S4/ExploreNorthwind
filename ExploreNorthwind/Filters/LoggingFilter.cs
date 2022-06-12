using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace ExploreNorthwind.Filters
{
    public class LoggingFilter: ActionFilterAttribute, IActionFilter
    {
        public bool LoggingParametersOn { set; get; }

        private readonly ILogger<LoggingFilter> _logger;

        public LoggingFilter(ILogger<LoggingFilter> logger, bool loggingParametersOn = false)
        {
            _logger = logger;
            LoggingParametersOn = loggingParametersOn;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            var parameters = new StringBuilder();
            foreach (var item in context.ActionArguments)
            {
                parameters.AppendLine($"Key: {item.Key}, Value: {item.Value}; ");
            }
            _logger.LogInformation($"Action {action} of controller {controller} started.");
            if (LoggingParametersOn) _logger.LogInformation($"Parameters: {parameters}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            _logger.LogInformation($"Action {action} of controller {controller} ended.");
        }
    }   
}
