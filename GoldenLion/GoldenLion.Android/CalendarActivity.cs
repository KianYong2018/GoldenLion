using System;
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

[assembly: Dependency(typeof(CalendarActivity))]
namespace GoldenLion.Droid
{
    [Activity(Label = "CalendarActivity")]
    public class CalendarActivity : Activity, ICalendar
    {
        Android.Widget.Button ButtonNotification;
        Android.Widget.DatePicker datePicker;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.CalendarPage); //Render the UI for CalendarPage

            ButtonNotification = FindViewById<Android.Widget.Button>(Resource.Id.buttonNotification);
            datePicker = FindViewById<Android.Widget.DatePicker>(Resource.Id.datePicker);

            ButtonNotification.Click += ButtonNotification_Click;
        }

        private void ButtonNotification_Click(object sender, EventArgs e)
        {
            GetDate();
            Finish();
        }

        public void DisplayCalendar()
        {
            var intent = new Intent(Android.App.Application.Context, typeof(CalendarActivity));
            Android.App.Application.Context.StartActivity(intent);
        }

        public string GetDate()
        {
            String date = datePicker.DateTime.ToLongDateString();
            Toast.MakeText(this, date, ToastLength.Short).Show();
            return date;
        }
    }
}