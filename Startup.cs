using EsentJsonStorage;
using Microsoft.AspNet.SignalR;
using Microsoft.Isam.Esent.Collections.Generic;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Owin;
using System;
using System.IO;

namespace Almhult
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration();                
                map.RunSignalR(hubConfiguration);
            });
            
            string contentPath = Path.Combine(Environment.CurrentDirectory, @"..\..");
            var baseUrl = "";
            app.UseStaticFiles(new Microsoft.Owin.StaticFiles.StaticFileOptions()
            {
                RequestPath = new PathString(baseUrl),
                FileSystem = new PhysicalFileSystem(contentPath)
            });

            app.Run(async context =>
            {

                var split = context.Request.Path.ToString().Split('/');
                var resource = (split.Length > 1) ? split[1] : "";
                var id = (split.Length > 2) ? split[2] : "";
                
                context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                context.Response.Headers["Access-Control-Allow-Headers"] = "x-requested-with";
                context.Response.Headers["Access-Control-Allow-Methods"] = "POST,PUT,OPTIONS,GET";
                
                if (context.Request.Method == "OPTIONS")
                {
                    await context.Response.WriteAsync("OK");
                }
                if (context.Request.Method == "POST")
                {
                    var formData = await context.Request.ReadFormAsync();
                    
                    var x = new StreamReader(context.Request.Body);
                    var s = x.ReadToEnd();

                    var result = "";

                    if (split.Length == 2) // resource/{payload} [post]
                    {
                        var obj = formData.Serialize();

                        using (var store = EsentJsonStorage.Storage.GetStore(resource))
                        {
                            result = store.Set(obj);
                            store.Dictionary.Flush();
                        }
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

                        using (var store = EsentJsonStorage.Storage.GetStore(resource))
                        {
                            result = store.Set(id, obj);
                            store.Dictionary.Flush();
                        }
                    }
                    else result = "Bad request";
                    await context.Response.WriteAsync(result);
                }
                if (context.Request.Method == "GET")
                {
                    if (split.Length == 3)  // resource/id [get]
                    {
                        using (var store = Storage.GetStore(resource))
                        {
                            var rev = context.Request.Query["rev"];
                            var result = "";
                            if (rev != null)
                            {
                                var revision = 0;
                                int.TryParse(rev.ToString(), out revision);
                                result = store.Get(id, revision);
                            }
                            else
                            {
                                result = store.Get(id);
                            }                            
                            await context.Response.WriteAsync(result);
                        }
                    }
                    if (split.Length == 2) // resource [get]
                    {
                        using (var store = Storage.GetStore(resource))
                        {
                            var result = store.GetAll(false);
                            await context.Response.WriteAsync(result);
                        }
                    }
                }
            });           

            //app.UseHandlerAsync((req, res) =>
            //{
            //});
        }
    }

}
