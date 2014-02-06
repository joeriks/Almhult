using PersistenceRest;
using Microsoft.AspNet.SignalR;
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
        public Startup()
        {
            AppDomain.CurrentDomain.Load(typeof(PersistenceHub).Assembly.FullName);
        }
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
            app.Map("/rest", map =>
            {
                map.UsePersistenceRest(new PersistenceRestMiddleware.PersistenceRestOptions {
                    RestStorage = new PersistenceRestEsentJsonStorage()
                });
            });

        }
    }

}
