using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnDocker.Filters
{
    public class CustomExceptionFilter : ActionFilterAttribute
    {
        private readonly LearnDockerSettings _apiSettings;
        public CustomExceptionFilter(IOptions<LearnDockerSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}
