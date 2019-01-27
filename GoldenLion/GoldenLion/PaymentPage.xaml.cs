using Android.Icu.Util;
using GoldenLion.Entity;
using GoldenLion.Managers;
using GoldenLion.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLion
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaymentPage : ContentPage
	{
        UserAccountManager userAccountManager;
        UsersViewModel usersViewModel;
        CalendarAttendanceManager SaveAttendance;

        public PaymentPage (IEnumerable<UserAccount> usersAccounts)
		{
            SaveAttendance = CalendarAttendanceManager.DefaultPayment;
            InitializeComponent();
            userAccountManager = UserAccountManager.DefaultUserAccount;
            usersViewModel = new UsersViewModel(usersAccounts);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //IEnumerable<UserAccount> users = await userAccountManager.GetUserBasedOnRoleAsync(false, null);
            BindingContext = usersViewModel;
        }

        async void Button_Clicked_1(object sender, EventArgs e) //This is Button_Clicked_1 event handler
        {
            List<CalendarAttendance> insertMultiplePayment = new List<CalendarAttendance>();
            IEnumerable<UserAccount> test = usersViewModel.GetSelectedUsers();
            foreach(UserAccount i in test)
            {
                CalendarAttendance calendarAttendance = new CalendarAttendance(); 
                calendarAttendance.DateTime = DateTime.Now.ToShortDateString();
                calendarAttendance.UserAccountID = i.IdUserAccount;
                insertMultiplePayment.Add(calendarAttendance);
            }

            foreach (CalendarAttendance i in insertMultiplePayment)
            {
                await SaveAttendance.SaveTaskAsync(i);
            }
        }
    }
}