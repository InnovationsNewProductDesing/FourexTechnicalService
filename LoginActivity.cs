
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


namespace Fourex.Droid
{
    [Activity(Label = "Fourex Technical Services")]
    public class LoginActivity : Activity
    {


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "Login" layout resource
            SetContentView(Resource.Layout.Login);

            //EditText userIDEtxt = FindViewById<EditText>(Resource.Id.userId);
            //EditText userPassEtxtx = FindViewById<EditText>(Resource.Id.userPassword);
            //Button logintBtn = FindViewById<Button>(Resource.Id.loginButton);
            //TextView Registertxt = FindViewById<TextView>(Resource.Id.registerText);

            //// Get test from UserId Field and Pass to UserId Variable
            //userIDEtxt.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            //{
            //    PublicVariables.userID = e.Text.ToString();

            //};

            //// Get test from UserPassword Field and Pass to UserPass Variable
            //userPassEtxtx.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            //{
            //    PublicVariables.userPass = e.Text.ToString();

            //};


            //// Call Login Method
            //logintBtn.Click += delegate
            //{
            //    var intent = new Intent(this, typeof(RemedyActivity));
            //    StartActivity(intent);
            //};

            //Registertxt.Click += delegate
            //{
            //    var intent = new Intent(this, typeof(RegisterActivity));
            //    StartActivity(intent);
            //};

        }
    }
}
