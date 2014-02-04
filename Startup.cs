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
            app.UseHandlerAsync((req, res) =>
            {
                var split = req.Path.Split('/'); // resource/key/value [POST]
                if (split.Length == 4)
                {
                    var resource = split[1];
                    var key = split[2];
                    var value = split[3];                    
                    FnX.EsentKeyValue.GetStore(resource).Set(key,value);
                    FnX.EsentKeyValue.GetStore(resource).Dictionary.Flush();
                    return res.WriteAsync("Set " + resource + " " + key + " " + value);

                }
                if (split.Length == 3)
                {
                    var resource = split[1]; // resource/key [GET] 
                    var key = split[2];
                    return res.WriteAsync(FnX.EsentKeyValue.GetStore(resource).Get(key));
                }
                if (split.Length == 2)
                {
                    var resource = split[1]; // resource [GET] 
                    return res.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(FnX.EsentKeyValue.GetStore(resource).Dictionary));
                }
                return res.WriteAsync("Bad request");
            });
        }
    }

}
