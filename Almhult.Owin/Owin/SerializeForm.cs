using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almhult
{
    public static class SerializeForm
    {
        public static string Serialize(this IFormCollection collection)
        {
            var list = new Dictionary<string, string>();
            foreach (var kv in collection)
            {                
                list.Add(kv.Key, kv.Value[0]);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
    }
}
