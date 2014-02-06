using EsentJsonStorage;
using PersistenceRest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceRest
{
    public class PersistenceRestEsentJsonStorage : IPersistenceRestStorage
    {

        public string Get(string resource, string id, int revision, bool getValue = false)
        {
            using (var store = Storage.GetStore(resource))
            {
                if (getValue) return store.GetValue(id, revision);
                return store.Get(id, revision);
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

        public string Put(string resource, string id, object obj)
        {
            using (var store = Storage.GetStore(resource))
            {
                return store.Set(id, obj);
            }

        }

        public void Delete(string resource)
        {
            using (var store = Storage.GetStore(resource))
            {
                store.Dictionary.Clear();
            }
        }
    }
}
