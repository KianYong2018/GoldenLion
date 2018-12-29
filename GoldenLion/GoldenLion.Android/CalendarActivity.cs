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

namespace GoldenLion.Droid
{
    [Activity(Label = "CalendarActivity")]
    public class CalendarActivity : Activity
    {
        Android.Widget.Button ButtonNotification;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.CalendarPage); //Render the UI for CalendarPage

            ButtonNotification = FindViewById<Android.Widget.Button>(Resource.Id.buttonNotification);

            ButtonNotification.Click += ButtonNotification_Click;
        }

        private void ButtonNotification_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Going back to Form Page", ToastLength.Short).Show();
            //Intent intent = new Intent(this, typeof(Menu));
            //Android.App.Application.Context.StartActivity(intent);
        }

        
    }
}