using Wangkanai.Detection.Services;

namespace Middleware.Middlewares
{
    public class BrowserMiddleware : IMiddleware
    {
       private readonly IDetectionService _detection;

        public BrowserMiddleware(IDetectionService detection)
        {
            _detection = detection; 
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var forbiddenBrowsers = new[] 
            {
                Wangkanai.Detection.Models.Browser.Chrome,
                Wangkanai.Detection.Models.Browser.Firefox
            };

            if (forbiddenBrowsers.Contains(_detection.Browser.Name))
            {
                context.Response.Redirect("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
            }

            await next(context);
        }
    }
}
