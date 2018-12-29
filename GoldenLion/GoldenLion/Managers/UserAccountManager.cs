/*
 * To add Offline Sync Support:
 * 1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 * 2) Uncomment the #define OFFLINE_SYNC_ENABLED
 * 
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
 */
 //#define OFFLINE_SYNC_ENABLED

using GoldenLion.Entity;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
#endif

namespace GoldenLion
{
    public partial class UserAccountManager
    {
        static UserAccountManager defaultInstance = new UserAccountManager();
        MobileServiceClient client;

#if OFFLINE_SYNC_ENABLED
        IMobileServiceSyncTable<UserAccount> userAccount;
#else
        IMobileServiceTable<UserAccount> userAccount;
#endif

        const string offlineDbPath = @"localstore.db";

        private UserAccountManager()
        {
            this.client = new MobileServiceClient(Constant.ApplicationURL);

#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore(offlineDbPath);
            store.DefineTable<UserAccount>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            this.client.SyncContext.InitializeAsync(store);

            this.userAccount = client.GetSyncTable<UserAccount>();
#else
            this.userAccount = client.GetTable<UserAccount>();
#endif
        }

        public static UserAccountManager DefaultUserAccount
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
            get { return userAccount is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<UserAccount>; }
        }

        public async Task<ObservableCollection<UserAccount>> GetUserAccountAsync(bool syncItems = false, string searchRole = "Members")
        {
            try
            {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                IEnumerable<UserAccount> items;
                if (string.IsNullOrEmpty(searchRole))
                {
                    items = await userAccount
                    .Where(userAccount => !userAccount.Deleted)
                    .ToEnumerableAsync();
                }
                else
                {
                    items = await userAccount
                    .Where(userAccount => !userAccount.Deleted && userAccount.Role == searchRole)
                    .ToEnumerableAsync();
                }
                return new ObservableCollection<UserAccount>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            return null;
        }

        public async Task<bool> CheckUserAccount(string username, string password)
        {
            //IEnumerable<UserAccount> When this query is executed, it will produced a sequence of zero or more UserAccount Object
            IEnumerable<UserAccount> store = await userAccount
                .Where(userAccount => userAccount.Username == username && userAccount.Password == password)
                .ToEnumerableAsync();

            if (store.Count() == 0)
            {
                return false;
            }
            else{
                return true;
            }
        }
        
        public async Task SaveTaskAsync(UserAccount item)
        {
            try
            {
                if (String.IsNullOrEmpty(item.IdUserAccount))
                {
                    await userAccount.InsertAsync(item);
                }
                else
                {
                    await userAccount.UpdateAsync(item);
                }
            }
            catch (Exception e)
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
            catch (MobileServicePushFailedException exc)
            {
                if(exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }
            
            // Simple error/conflict handing. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
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

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["idUserAccount"]);
                }
            }
        }
#endif
    }
}
