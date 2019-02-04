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
        int select;
        public Display()
        {
            InitializeComponent();
            CalendarAttendanceManager = CalendarAttendanceManager.DefaultCalendarAttendance;
            DateAndNameManager = new DateAndNameManager();
        }

        async void ButtonRefresh_Clicked(object sender, EventArgs e)
        {
            //Create a Dictionary that stores all the name 
            listViewDisplay.ItemsSource = await DateAndNameManager.DisplayNameAndDate();
        }

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(SearchBar.Text) && listViewDisplay.ItemsSource != null)
            {
                String searchText = SearchBar.Text;
            }
        }
    }
}