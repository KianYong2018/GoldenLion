using GoldenLion.Entity;
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
        
        public PaymentPage ()
		{
            InitializeComponent();

            BindingContext = new MainPageViewModel();

            userAccountManager = UserAccountManager.DefaultUserAccount;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((MainPageViewModel)BindingContext).OnAppearing();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            listView.ItemsSource = await userAccountManager.GetUserBasedOnRoleAsync(false, null);
        }
    }
}