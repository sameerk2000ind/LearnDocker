using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnDocker.Filters
{
    public class CustomAuthorization : ActionFilterAttribute
    {
        private readonly LearnDockerSettings _apiSettings;

        public CustomAuthorization(IOptions<LearnDockerSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            bool isAuthorized = false;

            var authKeyName = "apikey";
            var allowedAuthKeys = _apiSettings.ApiKey;

            var allowedKeysList = allowedAuthKeys.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (headers.Keys.Contains(authKeyName) && allowedKeysList.Any())
            {
                var header = headers.FirstOrDefault(x => x.Key == authKeyName).Value.FirstOrDefault();
                if (header != null)
                {
                    isAuthorized = Array.Exists(allowedKeysList, key => key.Equals(header));
                }
            }

            if(!isAuthorized)
            {
                context.Result = new ContentResult()
                {
                    Content = "Authorization has been denied.",
                    ContentType = "text/plain",
                    StatusCode = 401
                };
            }
        }

    }
}
