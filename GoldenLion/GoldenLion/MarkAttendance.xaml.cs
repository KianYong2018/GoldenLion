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

        public PaymentPage ()
		{
            SaveAttendance = CalendarAttendanceManager.DefaultCalendarAttendance;
            InitializeComponent();
            userAccountManager = UserAccountManager.DefaultUserAccount;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            IEnumerable<UserAccount> usersAccounts = await userAccountManager.GetUserBasedOnRoleAsync(false, null);
            usersViewModel = new UsersViewModel(usersAccounts);
            BindingContext = usersViewModel;
        }

        async void Button_Marked(object sender, EventArgs e) //This is Button_Clicked_1 event handler
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
            if(await SaveAttendance.CheckAndSave(insertMultiplePayment) == false)
            {
                await DisplayAlert("Alert", "Not Save. You have duplicate attendance marked already. Please check", "Okay");
            }
            else
            {
                var result = await DisplayAlert("Success", "Attendance has been successfully marked. Do you want to View them?", "Yes", "No");
                if(result == true)
                {
                    await Navigation.PushAsync(new Display());
                }
                else
                {
                    await Navigation.PopAsync();
                }
            }
        }
    }
}