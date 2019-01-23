﻿using GoldenLion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLion
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Menu : ContentPage
	{
        UserAccountManager UserAccountManager = UserAccountManager.DefaultUserAccount;
		public Menu ()
		{
			InitializeComponent ();
		}

        async void ButtonAttendance_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AttendancePage());
        }

        async void ButtonTestPayment_Clicked(object sender, EventArgs e)
        {
            IEnumerable<UserAccount> userAccounts = await UserAccountManager.GetUserBasedOnRoleAsync(false, null);
            await Navigation.PushAsync(new PaymentPage(userAccounts));
        }

        public void ButtonCalendar_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<ICalendar>().DisplayCalendar(); //Going into Xamarin.Android
        }

        async void ButtonTestLogout_Clicked(object sender, EventArgs e)
        {
            IEnumerable<UserAccount> DeviceType = await UserAccountManager.GetUserAccountAsync(Application.Current.Properties["UserName"].ToString());
            UserAccount LogoutUser = DeviceType.ElementAtOrDefault(0);
            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
            {
                LogoutUser.AndroidIOS = false;
            }
            else
            {
                LogoutUser.UWP = false;
            }
            await UserAccountManager.SaveTaskAsync(LogoutUser);
            await Navigation.PushAsync(new MainPage());
        }

        private void ButtonAttendance_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}