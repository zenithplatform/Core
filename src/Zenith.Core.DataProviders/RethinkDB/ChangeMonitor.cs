using RethinkDb.Driver;
using RethinkDb.Driver.Ast;
using RethinkDb.Driver.Model;
using RethinkDb.Driver.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zenith.Core.DataProviders.Events;

namespace Zenith.Core.DataProviders.Rethink
{
    public class ChangeMonitor<T> where T : class
    {
        Thread changeMonitorThread;
        public static RethinkDB R = RethinkDB.R;
        Connection _connection = null;
        private static CancellationTokenSource stopMonitor = new CancellationTokenSource();

        string _dbName = "", _tableName = "";

        public ChangeMonitor(string dbName, string tableName)
        {
            _dbName = dbName;
            _tableName = tableName;
        }

        private void InitConnection()
        {
            _connection = R.Connection()
                           .Hostname("192.168.0.101")
                           .Port(RethinkDBConstants.DefaultPort + 1)
                           .Timeout(60)
                           .Connect();
        }

        public void Start()
        {
            changeMonitorThread = new Thread(MonitorRoutine);
            changeMonitorThread.Start();
        }

        public void Stop()
        {
            stopMonitor.Cancel();
            changeMonitorThread.Join();
        }

        private async void MonitorRoutine()
        {
            try
            {
                Cursor<Change<T>> changes = await R.Db(_dbName)
                                                   .Table(_tableName)
                                                   .RunChangesAsync<T>(_connection, stopMonitor.Token);

                foreach(Change<T> change in changes)
                {
                    DataEventsGateway.Instance.Trigger(new DataChangedEvent());
                }
            }
            catch (AggregateException exc)
            {
                if (!(exc.InnerException is TaskCanceledException))
                    throw;
            }
        }
    }
}
