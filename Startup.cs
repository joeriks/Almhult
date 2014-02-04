using Microsoft.AspNet.SignalR;
using Microsoft.Isam.Esent.Collections.Generic;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;


using System;
using System.IO;

namespace SignalrDataSelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/data", map =>
            {
                map.UseHandlerAsync((req, res) =>
                {
                    var split = req.Path.Split('/');
                    if (split.Length == 3)
                    {
                        FnX.EsentKeyValue.GetStore().Set(split[1], split[2]);
                        return res.WriteAsync("Set " + split[1] + ":" + split[2]);
                    }

                    res.ContentType = "text/plain";
                    return res.WriteAsync("Hello, World!");

                });
                map.UseCors(CorsOptions.AllowAll);
                
            });

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
        }
    }

}
