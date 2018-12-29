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
		public Menu ()
		{
			InitializeComponent ();
		}

        async void ButtonAttendance_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AttendancePage());
        }

        private void ButtonTestPayment_Clicked(object sender, EventArgs e)
        {

        }

        public void ButtonCalendar_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<ICalendar>().DisplayCalendar();
        }
    }
}