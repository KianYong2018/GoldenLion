using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoldenLion
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var entry = new Entry { Placeholder = "Username", PlaceholderColor = Color.Olive };

        }

        private void ButtonEntry_Clicked(object sender, EventArgs e)
        {
            if(EntryUsername.Text != null && EntryPassword.Text != null)
            {
                LabelToast.Text = "Logining in. Please wait";
            }
            else
            {
                LabelToast.Text = "Do not leave any blanks";
            }
        }

        async void ButtonSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TodoList()); //Go to SignUp.xaml
        }
    }
}
