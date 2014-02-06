using System;
namespace Almhult
{
    internal interface IPersistenceRestStorage
    {
        string Get(string resource);
        string Get(string resource, string id, int revision);
        string Post(string resource, object value);
        string Put(string resource, string id, string obj);
    }
}
