using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.DataProviders.SQLServer
{
    public class GenericDbContext : DbContext
    {
        public DbSet _currentDbSet = null;

        public GenericDbContext(ConnectionParams connectionParams):
            base(connectionParams.ConnectionString)
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.EnsureTransactionsForFunctionsAndCommands = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = true;
        }

        public void OpenConnection()
        {
            this.Database.Connection.Open();
        }

        public void CloseConnection()
        {
            this.Database.Connection.Close();
            this.Database.Connection.Dispose();
        }

        public bool IsAvailable
        {
            get { return this.Database.Connection.State == ConnectionState.Open; }
        }

        public void CleanUp()
        {
            if(this.Database.Connection.State == ConnectionState.Open)
                CloseConnection();

            this.Dispose();
        }

        public override DbSet Set(Type entityType)
        {
            _currentDbSet = base.Set(entityType);

            return _currentDbSet;
        }
    }
}
