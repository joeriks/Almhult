using Microsoft.Isam.Esent.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalrDataSelfHost
{
    public class Persistence
    {
        private static ConcurrentDictionary<string, PersistentDictionary<string,string>> persistenceDictionary = new ConcurrentDictionary<string,PersistentDictionary<string,string>>();
        public static PersistentDictionary<string, string> PersistentDictionary(string name)
        {

            if (persistenceDictionary.ContainsKey(name)) return persistenceDictionary[name];
            string dataPath = Path.Combine(Environment.CurrentDirectory, @"..\App_Data\" + name);
            persistenceDictionary[name] = new PersistentDictionary<string, string>(dataPath);
            return persistenceDictionary[name];
        }
        public static string Get(string dictionaryName, string key)
        {
            var dictionary = PersistentDictionary(dictionaryName);
            if (dictionary.ContainsKey(key)) return dictionary[key];
            return null;
        }
        public static IDictionary<string, string> All(string dictionaryName)
        {
            return PersistentDictionary(dictionaryName);
        }
        public static void Set(string dictionaryName, string key, object value)
        {
            var dictionary = PersistentDictionary(dictionaryName);
            dictionary[key] = value.ToString();

            // for some reason I need to flush data to disk in Owin context
            dictionary.Flush();
        }


    }
}
