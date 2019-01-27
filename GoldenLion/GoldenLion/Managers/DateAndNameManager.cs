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
        public async Task<ObservableCollection<DateAndName>> DisplayNameAndDate ()
        {
            CalendarAttendance = CalendarAttendanceManager.DefaultPayment;
            UserAccountManager = UserAccountManager.DefaultUserAccount;

            //The line below has some errors.
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
    }
}
