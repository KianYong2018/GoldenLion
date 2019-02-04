using GoldenLion.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoldenLion
{
    public partial class MainPage : ContentPage
    {
        UserAccountManager userAccountManager;
        public MainPage()
        {
            InitializeComponent();
            var entry = new Entry { Placeholder = "Username", PlaceholderColor = Color.Olive };
            userAccountManager = UserAccountManager.DefaultUserAccount;
        }

        async void ButtonEntry_Clicked(object sender, EventArgs e)
        {
            string username = EntryUsername.Text;
            string password = EntryPassword.Text;
            ButtonEntry.IsEnabled = false;
            LabelToast.Text = "Logining in. Please wait";
            if (await userAccountManager.CheckUserAccount(username, password, Device.RuntimePlatform))
            {
                Application.Current.Properties["UserName"] = userAccountManager.GetUserName();
                await Navigation.PushAsync(new Menu());
                ClearEntry();
            }
            else
            {
                ClearEntry();
                await DisplayAlert("Alert", "Incorrect Password or Username", "Okay");
            }
        }

        private void ClearEntry()
        {
            EntryUsername.Text = string.Empty;
            EntryPassword.Text = string.Empty;
            LabelToast.Text = string.Empty;
        }

        async void ButtonSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp()); //Go to SignUp.xaml
        }
    }
}
