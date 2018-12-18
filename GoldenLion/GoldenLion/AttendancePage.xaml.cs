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
	public partial class AttendancePage : ContentPage
	{
		public AttendancePage ()
		{
			InitializeComponent ();
		}

        private void ButtonConfirm_Clicked(object sender, EventArgs e)
        {

        }
    }
}