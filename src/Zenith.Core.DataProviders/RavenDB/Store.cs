using Raven.Client;
using Raven.Client.Document;
using System;

namespace Zenith.Core.DataProviders.RavenDB
{
    public class StoreInstance : IDisposable
    {
        public IDocumentStore Store { get; private set; }

        public StoreInstance(ConnectionParams connectionParams)
        {
            Store = new DocumentStore
            {
                Url = string.Concat(connectionParams.Url, ":", connectionParams.Port),
                DefaultDatabase = connectionParams.Database

            }.Initialize();
        }

        public void Dispose()
        {
            Store.Dispose();
        }
    }
}
