using GoldenLion.Entity;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenLion.Managers
{
    public class CalendarAttendanceManager
    {
        static CalendarAttendanceManager defaultInstance = new CalendarAttendanceManager();
        MobileServiceClient client;

#if OFFLINE_SYNC_ENABLED
        IMobileServiceSyncTable<CalendarAttendance> calendarAttendance;
#else
        IMobileServiceTable<CalendarAttendance> calendarAttendance;

        IMobileServiceTable<UserAccount> userAccount; //Testing
#endif

        const string offlineDbPath = @"localstore.db";

        private CalendarAttendanceManager()
        {
            this.client = new MobileServiceClient(Constant.ApplicationURL);

#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore(offlineDbPath);
            store.DefineTable<CalendarAttendance>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            this.client.SyncContext.InitializeAsync(store);

            this.calendarAttendance = client.GetSyncTable<CalendarAttendance>();
#else
            this.calendarAttendance = client.GetTable<CalendarAttendance>();
#endif
        }

        public static CalendarAttendanceManager DefaultPayment
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
            get { return calendarAttendance is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<CalendarAttendance>; }
        }

        public async Task SaveTaskAsync(CalendarAttendance item)
        {
            try
            {
                if (String.IsNullOrEmpty(item.Id))
                {
                    await calendarAttendance.InsertAsync(item);
                }
                else
                {
                    await calendarAttendance.UpdateAsync(item);
                }
            }catch(Exception e)
            {
                Debug.WriteLine("Save error: {0}", new[] { e.Message });
                if(e.InnerException != null)
                {
                    Debug.WriteLine("Inner Exception: {0}", new[] { e.InnerException });
                }
            }
        }

        public async Task<ObservableCollection<CalendarAttendance>> GetCalendarAttendance()
        {
            try
            {
                IEnumerable<CalendarAttendance> items;
                items = await calendarAttendance.Where(calendarAttendance => !calendarAttendance.Deleted)
                    .ToEnumerableAsync();
                return new ObservableCollection<CalendarAttendance>(items);
            }catch(Exception e)
            {
                Debug.WriteLine("Retrieve Error: " + e);
            }
            return null;
        }

        public async Task<ObservableCollection<UserAccount>> GetNameAndDate() //Testing this out 
        {
            try
            {//IMobileServiceTable currently does not support join and 
             //can only query one table at a time
                List<UserAccount> users = new List<UserAccount>();
                List<CalendarAttendance> calendars = new List<CalendarAttendance>();
                calendars = await calendarAttendance.Where(calendarAttendance =>
                !calendarAttendance.Deleted).ToListAsync();

                userAccount = client.GetTable<UserAccount>();

                users = await userAccount.Where(userAccount => !userAccount.Deleted)
                    .ToListAsync();

                var query = from user in users join calendar in calendars on
                            user.IdUserAccount equals calendar.UserAccountID
                            where user.IdUserAccount == calendar.UserAccountID
                            select user;
                return new ObservableCollection<UserAccount>(query);
            }
            catch(Exception e)
            {
                Debug.WriteLine("Statement Error: " + e);
                return null;
            }
        }

        public async Task<ObservableCollection<CalendarAttendance>> GetCalendarAttendances()
        {
            try
            {
                IEnumerable<CalendarAttendance> Attendances;
                Attendances = await calendarAttendance.Where(calendarAttendance => !calendarAttendance.Deleted)
                    .ToEnumerableAsync();
                return new ObservableCollection<CalendarAttendance>(Attendances);
            }catch(Exception e)
            {
                Debug.WriteLine("Retrieve Error: {0}", new[] { e.Message });
            }
            return null;
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
