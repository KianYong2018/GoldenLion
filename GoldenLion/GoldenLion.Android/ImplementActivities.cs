/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GoldenLion.Droid;
using Xamarin.Forms;

[assembly:Dependency(typeof(ImplementActivities))]
namespace GoldenLion.Droid
{
    public class ImplementActivities : ICalendar
    {
        public void DisplayCalendar()
        {
            var intent = new Intent(Android.App.Application.Context,typeof(CalendarActivity));
            Android.App.Application.Context.StartActivity(intent);
        }

        public string GetDate()
        {
            Intent intentReceieved = new Intent();
            return intentReceieved.GetStringExtra("date");
        }
    }
}*/