using Microsoft.AspNet.SignalR;
using Microsoft.Isam.Esent.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsentJsonStorage;

namespace SignalrDataSelfHost
{
    public class PersistenceHub : Hub
    {
        public string Get(string key)
        {
            return Storage.GetStore().Get(key);
        }
        public string Get(string key, int revision)
        {
            return Storage.GetStore().Get(key, revision);
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
        public string All()
        {
            return All(false);
        }
        public void Clear()
        {
            using (var store = Storage.GetStore())
            {
                store.Dictionary.Clear();
            }
        }
        public string All(bool excludeSystemProperties)
        {
            return Storage.GetStore().GetAll(excludeSystemProperties);
        }
    }
}
