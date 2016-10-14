using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace App.Waes.ScalableWeb.WebApi.Server.Middleware
{
    public class LogMiddleware : OwinMiddleware
    {
        public LogMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            Trace.WriteLine($"Request: {context.Request.Method} {context.Request.Uri}");
            await Next.Invoke(context);
        }
    }
}