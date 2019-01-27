using GoldenLion.Entity;
using GoldenLion.Managers;
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
    public partial class Display : ContentPage
    {
        DateAndNameManager DateAndNameManager;
        CalendarAttendanceManager CalendarAttendanceManager;
        public Display()
        {
            InitializeComponent();
            CalendarAttendanceManager = CalendarAttendanceManager.DefaultPayment;
            DateAndNameManager = new DateAndNameManager();
        }

        async void ButtonRefresh_Clicked(object sender, EventArgs e)
        {
            
            listViewDisplay.ItemsSource = await DateAndNameManager.DisplayNameAndDate();
        }

    }
}