using GoldenLion.Entity;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace GoldenLion.Managers
{
    public class PaymentManager
    {
        static PaymentManager defaultInstance = new PaymentManager();
        MobileServiceClient client;

#if OFFLINE_SYNC_ENABLED
        IMobileServiceSyncTable<Payment> payment;
#else
        IMobileServiceTable<Payment> payment;
#endif

        const string offlineDbPath = @"localstore.db";

        private PaymentManager()
        {
            this.client = new MobileServiceClient(Constant.ApplicationURL);

#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore(offlineDbPath);
            store.DefineTable<Payment>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            this.client.SyncContext.InitializeAsync(store);

            this.payment = client.GetSyncTable<Payment>();
#else
            this.payment = client.GetTable<Payment>();
#endif
        }

        public static PaymentManager DefaultPayment
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public bool IsOfflineEnabled
        {
            get { return payment is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<Payment>; }
        }

        public async Task SaveTaskAsync(Payment item)
        {
            try
            {
                if (String.IsNullOrEmpty(item.Id))
                {
                    await payment.InsertAsync(item);
                }
                else
                {
                    await payment.UpdateAsync(item);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("Save error: {0}", new[] { e.Message });
            }
        }

#if OFFLINE_SYNC_ENABLED
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            
            try
            {
                await this.client.SyncContext.PushAsync();
                
                await this.todoTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allTodoItems",
                    this.todoTable.CreateQuery());
            }
            catch(MobileServicePushFailedException exc)
            {
                if(exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            //Simple error/conflict handing. A real application would handle the various errors like network conditions,
            //server conflicts and others via the IMobileServiceSyncHandler.
            if(syncErrors != null)
            {
                foreach(var error in syncErrors)
                {
                    if(error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        //Discord local change.
                        await error.CancelAndDiscardItemAsync();
                    }
                    
                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }
#endif
    }
}
