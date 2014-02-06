using Microsoft.AspNet.SignalR;
using Microsoft.Isam.Esent.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsentJsonStorage;
using Newtonsoft.Json.Linq;

namespace SignalrDataSelfHost
{
    public class PersistenceHub : Hub
    {
        internal static void MessageAdd(string resource, string id, object value)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<PersistenceHub>();
            context.Clients.All.messageAdd(resource, id, value);
        }
        public object Get(string key)
        {
            return JObject.Parse(Storage.GetStore().Get(key));
        }
        public object GetValue(string key)
        {
            var result = Storage.GetStore().GetValue(key);
            try
            {
                return JObject.Parse(result);

            }
            catch (Exception)
            {
                return result;
            }
        }
        public object Get(string key, int revision)
        {
            return JObject.Parse(Storage.GetStore().Get(key, revision));
        }
        public string Set(string id, object value)
        {
            using (var store = Storage.GetStore())
            {
                var result = store.Set(id, value);
                store.Dictionary.Flush();
                return result;
            }
        }
        public string Set(object value)
        {
            return Set("", value);
        }
        public void Clear()
        {
            using (var store = Storage.GetStore())
            {
                store.Dictionary.Clear();
            }
        }
        public string GetAll()
        {
            return Storage.GetStore().GetAll();
        }
    }
}
