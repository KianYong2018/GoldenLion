using GoldenLion.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenLion.Managers
{
    class DateAndNameManager
    {
        CalendarAttendanceManager CalendarAttendance;
        UserAccountManager UserAccountManager;

        //This can be improved for better efficiently
        public async Task<ObservableCollection<DateAndName>> DisplayNameAndDate ()
        {
            CalendarAttendance = CalendarAttendanceManager.DefaultCalendarAttendance;
            UserAccountManager = UserAccountManager.DefaultUserAccount;

            IEnumerable<UserAccount> u = await UserAccountManager.GetUserBasedOnRoleAsync(false, null);
            List<UserAccount> users = new List<UserAccount>(u);

            IEnumerable<CalendarAttendance> c = await CalendarAttendance.GetCalendarAttendance();
            List<CalendarAttendance> date = new List<CalendarAttendance>(c);

            DateAndName dateAndName;
            List<DateAndName> dateAndNames = new List<DateAndName>(); ;
            var query = from user in users
                        join calendar in date on
                        user.IdUserAccount equals calendar.UserAccountID
                        where user.IdUserAccount == calendar.UserAccountID
                        select new { User = user.Name, Date = calendar.DateTime };
            
            foreach(var info in query)
            {
                dateAndName = new DateAndName();
                dateAndName.Date = info.Date;
                dateAndName.Name = info.User;
                dateAndNames.Add(dateAndName);
            }
            return new ObservableCollection<DateAndName>(dateAndNames);
        }

        //Need to do the task below before this
        //(Create a method specifically for marking attendance and also checking if there are changes and update the page when necessary)
        async Task<ObservableCollection<DateAndName>> SearchSpecificName(String Name)
        {
            return null;
        }
    }
}
