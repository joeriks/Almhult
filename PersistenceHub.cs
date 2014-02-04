using Microsoft.AspNet.SignalR;
using Microsoft.Isam.Esent.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalrDataSelfHost
{
    public class PersistenceHub : Hub
    {
        public string Get(string key)
        {
            return FnX.EsentKeyValue.GetStore().Get(key);
        }
        public void Set(string key, object value)
        {
            FnX.EsentKeyValue.GetStore().Set(key, value);
        }
        public IDictionary<string,string> All()
        {
            return Persistence.All("default");
        }
    }
}
