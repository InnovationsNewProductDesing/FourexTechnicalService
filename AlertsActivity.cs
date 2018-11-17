using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Fourex.Droid
{
    [Activity(Label = "Alerts")]

    public class AlertsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Alerts);

    //        TextView redCol1TextView = FindViewById<TextView>(Resource.Id.redCol1);
    //        TextView redCol2TextView = FindViewById<TextView>(Resource.Id.redCol2);
    //        TextView orangeCol1TextView = FindViewById<TextView>(Resource.Id.orangeCol1);
    //        TextView orangeCol2TextView = FindViewById<TextView>(Resource.Id.orangeCol2);
    //        TextView greenCol1TextView = FindViewById<TextView>(Resource.Id.greenCol1);
    //        TextView greenCol2TextView = FindViewById<TextView>(Resource.Id.greenCol2);

    //        var redCol1Txt = "Westfield London 0048" + "\n" + "\n"
    //                        + "Angel 0003" + "\n" + "\n";
    //        redCol1TextView.Text = redCol1Txt;

    //        var redCol2Txt = "145" + "\n" + "\n"
    //                        + "103" + "\n" + "\n";
    //        redCol2TextView.Text = redCol2Txt;
            
    //        var orangeCol1txtx = "Croydon 0027" + "\n" + "\n"
    //                        + "Stansted 0016" + "\n" + "\n"
    //                         + "Uxbridge (UX1) 0046" + "\n" + "\n"
    //                          + "London Gateway 0006" + "\n" + "\n"
    //                          + "Kings Cross (KC2) 0023" + "\n" + "\n";
    //        orangeCol1TextView.Text = orangeCol1txtx;

    //        var orangeCol2Txt = "46" + "\n" + "\n"
    //                        + "41" + "\n" + "\n"
    //                        + "38" + "\n" + "\n"
    //                        + "31" + "\n" + "\n"
    //                        + "29" + "\n" + "\n";
    //        orangeCol2TextView.Text = orangeCol2Txt;



    //        var greenCol1txt = "Oxford Circus 0021" + "\n" + "\n"
    //                        + "Brent Cross (BC1) 0028" + "\n" + "\n";
    //        greenCol1TextView.Text = greenCol1txt;

    //        var greenCol2txt = "18" + "\n" + "\n"
    //                        + "15" + "\n" + "\n";
    //        greenCol2TextView.Text = greenCol2txt;

    //    }

    //    public override bool OnCreateOptionsMenu(IMenu menu)
    //    {
    //        MenuInflater.Inflate(Resource.Menu.menu, menu);
    //        return base.OnCreateOptionsMenu(menu);
    //    }

    //    public override bool OnOptionsItemSelected(IMenuItem item)
    //    {
    //        int id = item.ItemId;

    //        if (id == Resource.Id.nav_home)
    //        {
    //            var intent = new Intent(this, typeof(WelcomeActivity));
    //            StartActivity(intent);
    //            return true;
    //        }

    //        return base.OnOptionsItemSelected(item);
      }

    }
}