using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MailKit;
using MimeKit;

namespace Fourex.Droid
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        string name;
        string email1;
        string email2;
        string pin1;
        string pin2;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Registration);

            EditText NameEtxt = FindViewById<EditText>(Resource.Id.name);
            EditText Email1Etxt = FindViewById<EditText>(Resource.Id.email1);
            EditText Email2Etxt = FindViewById<EditText>(Resource.Id.email2);
            EditText Pin1Etxt = FindViewById<EditText>(Resource.Id.pin1);
            EditText Pin2Etxt = FindViewById<EditText>(Resource.Id.pin2);
            Button RegBtn = FindViewById<Button>(Resource.Id.registerButton);


            NameEtxt.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {

                name = e.Text.ToString();
            };

            Email1Etxt.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {

                email1 = e.Text.ToString();
            };

            Email2Etxt.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {

                email2 = e.Text.ToString();
            };

            Pin1Etxt.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {

                pin1 = e.Text.ToString();
            };

            Pin2Etxt.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {

                pin2 = e.Text.ToString();
            };

            RegBtn.Click +=  async delegate

            {
                if(String.IsNullOrWhiteSpace(name))
                {
                    Toast.MakeText(Application.Context, "Please enter your name", ToastLength.Long).Show();

                }

                else if (email1 != email2)
                {
                    Toast.MakeText(Application.Context, "Email addresses do not match", ToastLength.Long).Show();
                }
                else if (String.IsNullOrWhiteSpace(email1))
                {
                    Toast.MakeText(Application.Context, "Please enter a valid email address", ToastLength.Long).Show();
                }
                
                else if (pin1 != pin2)
                {
                    Toast.MakeText(Application.Context, "PINs do not match", ToastLength.Long).Show();
                }
                else if(String.IsNullOrWhiteSpace(pin1))
                {
                    Toast.MakeText(Application.Context, "Please enter a 4-digit PIN", ToastLength.Long).Show();

                }

                else
                {
                    ProgressDialog progressDialog = new ProgressDialog(this);
                    progressDialog.SetMessage("Please wait...");
                    progressDialog.Show();

                    await Register();

                    progressDialog.Dispose();

                }


            };




        }


        public async Task Register()
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("jedauret@yahoo.com");
                message.To.Add(new MailAddress("jedauret6@gmail.com"));
                message.IsBodyHtml = true;
                message.Subject = "FOUREX - Registration Request";

                string html = "The following user is requesting registration to Fourex Technical Services." + "\n" + "Name: " + name + "\n" + "Email: " + name + " " +
                "<form method='post' action='http://localhost:59089/api/Tech?Email=" + email1 + "&Password=" + pin1 + "&Name=" + name + "' >" +
                "<button name='subject' type='submit' value='Accept User'>Accept User</button>" +
                "</form>";


                message.Body = html;

                SmtpClient smtp = new SmtpClient("smtp.mail.yahoo.com", 587);
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("jedauret@yahoo.com", "Saigon123#");

                 smtp.SendAsync(message,null);
               

            }
            

            catch (Exception e)
            {

                Toast.MakeText(Application.Context, "Error, please try again", ToastLength.Long).Show();

                System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);

            }
        }
    }
 }
