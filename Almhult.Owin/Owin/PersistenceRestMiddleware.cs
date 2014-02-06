using EsentJsonStorage;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almhult
{
    public static class PersistenceRestMiddlewareEstensions
    {
        public static void UsePersistenceRest(
        this IAppBuilder app, PersistenceRestMiddleware.PersistenceRestOptions options = null)
        {
            options = options ?? new PersistenceRestMiddleware.PersistenceRestOptions();
            app.Use<PersistenceRestMiddleware>(options);
        }
    }
    public class PersistenceRestMiddleware
    {
        public class PersistenceRestOptions
        {
            public string AccessControlAllowOrigin { get; set; }
            internal IPersistenceRestStorage RestStorage { get; set; }
            public PersistenceRestOptions()
            {
                AccessControlAllowOrigin = "*";
                RestStorage = new PersistenceRestEsentJsonStorage();
            }
        }

        private readonly Func<IDictionary<string, object>, Task> _next;
        private PersistenceRestOptions _options;

        public PersistenceRestMiddleware(Func<IDictionary<string, object>, Task> next, PersistenceRestOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            if (environment == null)
            {
                throw new ArgumentNullException("environment");
            }
            var context = new OwinContext(environment);

            var split = context.Request.Path.ToString().Split('/');
            var resource = (split.Length > 1) ? split[1] : "";
            var id = (split.Length > 2) ? split[2] : "";

            context.Response.Headers["Access-Control-Allow-Origin"] = _options.AccessControlAllowOrigin;
            context.Response.Headers["Access-Control-Allow-Headers"] = "x-requested-with";
            context.Response.Headers["Access-Control-Allow-Methods"] = "POST,PUT,OPTIONS,GET";

            if (context.Request.Method == "OPTIONS")
            {
                await context.Response.WriteAsync("OK");
            }
            if (context.Request.Method == "POST")
            {
                var formData = await context.Request.ReadFormAsync();

                var result = "";

                if (split.Length == 2) // resource/{payload} [post]
                {
                    var obj = formData.Serialize();
                    _options.RestStorage.Post(resource, obj);

                }
                else result = "Bad request";
                await context.Response.WriteAsync(result);
            }
            if (context.Request.Method == "PUT")
            {
                var formData = await context.Request.ReadFormAsync();
                var result = "";

                if (split.Length == 3) // resource/id/{payload} [put]
                {
                    var obj = formData.Serialize();
                    result = _options.RestStorage.Put(resource, id, obj);
                }
                else result = "Bad request";
                await context.Response.WriteAsync(result);
            }
            if (context.Request.Method == "GET")
            {
                var result = "";
                if (split.Length == 3)  // resource/id [get]
                {
                    var rev = context.Request.Query["rev"];
                    var revision = 0;
                    if (rev != null)
                        int.TryParse(rev.ToString(), out revision);
                    result = _options.RestStorage.Get(resource, id, revision);

                }
                else if (split.Length == 2) // resource [get]
                {
                    result = _options.RestStorage.Get(resource);
                }
                else
                {
                    result = "Bad request";
                }
                await context.Response.WriteAsync(result);

            }

            await _next(environment);
        }
    }
}
