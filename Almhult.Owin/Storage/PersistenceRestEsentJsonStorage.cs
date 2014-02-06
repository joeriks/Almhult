using EsentJsonStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almhult
{
    public class PersistenceRestEsentJsonStorage : IPersistenceRestStorage
    {

        public string Get(string resource, string id, int revision)
        {
            using (var store = Storage.GetStore(resource))
            {
                var result = store.Get(id, revision);
                return result;
            }

        }
        public string Get(string resource)
        {
            using (var store = Storage.GetStore(resource))
            {
                return store.GetAll();
            }

        }
        public string Post(string resource, object value)
        {
            using (var store = Storage.GetStore(resource))
            {
                return store.Set(value);
            }

        }

        public string Put(string resource, string id, string obj)
        {
            using (var store = Storage.GetStore(resource))
            {
                return store.Set(id, obj);
            }

        }
    }
}
