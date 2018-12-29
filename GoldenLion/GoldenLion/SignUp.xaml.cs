using GoldenLion.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLion
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUp : ContentPage
	{
        UserAccountManager manager;

		public SignUp ()
		{
            InitializeComponent ();
            try
            {
                manager = UserAccountManager.DefaultUserAccount;
            }
            catch(Exception e)
            {
                Debug.WriteLine("Error:" + e.InnerException);
            }
        }

        async Task AddAccount(UserAccount userAccount)
        {
            await manager.SaveTaskAsync(userAccount);
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            var todo = new UserAccount { Name = EntryName.Text, Username = EntryUsername.Text, Password = EntryPassword.Text, Role = EntryRole.Text};
            await AddAccount(todo);
            EntryName.Text = string.Empty;
            EntryUsername.Text = string.Empty;
            EntryPassword.Text = string.Empty;
            EntryRole.Text = string.Empty;
        }
    }
}