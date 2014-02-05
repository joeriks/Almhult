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
        public string Set(string key, string value)
        {
            return Storage.GetStore().Set(key, value);
        }
        public IDictionary<string,string> All()
        {
            return Storage.GetStore().GetAll<string>();
        }
    }
}
