using System;
namespace PersistenceRest
{
    public interface IPersistenceRestStorage
    {
        void Delete(string resource);
        string Get(string resource);
        string Get(string resource, string id, int revision, bool getValue = false);
        string Post(string resource, object value);
        string Put(string resource, string id, object obj);
    }
}
