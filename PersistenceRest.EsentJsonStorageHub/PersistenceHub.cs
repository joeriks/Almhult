using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PersistenceRest
{
    public class PersistenceHub : Hub
    {
        private const string resource = "default";
        private PersistenceRest.PersistenceRestEsentJsonStorage storage = new PersistenceRestEsentJsonStorage();

        public static void MessageAdd(string resource, string id, object value)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<PersistenceHub>();
            context.Clients.All.messageAdd(resource, id, value);
        }
        public object Get(string id)
        {
            return storage.Get(resource, id, 0, true);
        }
        public object GetValue(string id)
        {
            var result = storage.Get(resource, id, 0, true);
            try
            {
                return JObject.Parse(result);
            }
            catch (Exception)
            {
                return result;
            }
        }
        public object Get(string id, int revision)
        {
            return JObject.Parse(storage.Get(resource, id, revision));
        }
        public string Set(string id, object value)
        {
            return storage.Put(resource, id, value);
        }
        public string Set(object value)
        {
            return Set("", value);
        }
        public void Clear()
        {
            storage.Delete(resource);
        }
        public string GetAll()
        {
            return storage.Get(resource);
        }
    }
}
