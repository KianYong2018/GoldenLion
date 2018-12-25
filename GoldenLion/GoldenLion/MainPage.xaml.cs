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

        private async void ButtonEntry_Clicked(object sender, EventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(EntryUsername.Text) && EntryPassword.Text != null)
            {
                LabelToast.Text = "Logining in. Please wait";
            }
            else
            {
                await DisplayAlert("Alert","Incorrect Password or Username","Okay");
            }
        }

        async void ButtonSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp()); //Go to SignUp.xaml
        }

        async void ButtonTestAttendancePage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AttendancePage());
        }
    }
}
