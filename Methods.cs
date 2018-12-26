using System;
using System.Threading.Tasks;
using MimeKit;
using MailKit;
using System.Collections;
using RestSharp;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Android.Widget;
using Android.App;
using Android.Support.V7.App;
using Android.OS;

namespace Fourex.Droid
{

    public class Methods
    {
        public static async Task RemedySubmit()

        { 
            try
            {
           
            var m = new MimeMessage();
           

            m.From.Add(new MailboxAddress("fourex@inpd.co.za"));


            m.To.Add(new MailboxAddress("fourex-rm-1@inpd.co.za"));
            m.To.Add(new MailboxAddress("fourex-rm-2@inpd.co.za"));
            m.To.Add(new MailboxAddress("fourex-rm-3@inpd.co.za"));

            m.To.Add(new MailboxAddress(PublicVariables.userID));



            m.Subject = "Fourex - " 
                        + PublicVariables.selectedKiosk 
                        + "->" 
                        + "REMEDY," 
                        + PublicVariables.selectedComponent 
                        +"," 
                        + PublicVariables.selectedError 
                        + "," 
                        +PublicVariables.selectedRemedy 
                        + "," 
                        + PublicVariables.userID 
                        + "," 
                        + PublicVariables.notes 
                        + "," 
                        + PublicVariables.TimeStamp
                        +", time=" 
                        + PublicVariables.RemedyHours 
                        + ":"  
                        +PublicVariables.RemedyMinutes
                        +":00"
                        ;


            BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "";
                

            m.Body = bodyBuilder.ToMessageBody();
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    var Username = "fourex@inpd.co.za";
                    var Password = "1nn0_Ques2018";

                    await client.ConnectAsync("smtp.inpd.co.za", 587,MailKit.Security.SecureSocketOptions.None);
                       
                    client.AuthenticationMechanisms.Remove("XOAUTH2");


                        await client.AuthenticateAsync(Username, Password)
                            .ConfigureAwait(false);


                    await client.SendAsync(m).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                    PublicVariables.RemedyHours = "00";
                    PublicVariables.RemedyMinutes = "01";

                    PublicVariables.ReturnMessage = "Completed";
                    PublicVariables.notes = "";
                    System.Diagnostics.Debug.WriteLine(m);
                }

            }
            catch (ServiceNotConnectedException e)
                    {
                        PublicVariables.ReturnMessage = e.Message;
                System.Diagnostics.Debug.WriteLine("ServiceNotConnectedException: " + e.Message);
                    }

           catch (Exception e)
                    {
                            PublicVariables.ReturnMessage = e.Message;
                System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);

            }

        }
        
        public static async Task NotificationSubmit()

        {
            try
            {

                var m = new MimeMessage();


                m.From.Add(new MailboxAddress("fourex@inpd.co.za"));

                m.To.Add(new MailboxAddress("fourex-rm-1@inpd.co.za"));
                m.To.Add(new MailboxAddress("fourex-rm-2@inpd.co.za"));
                m.To.Add(new MailboxAddress("fourex-rm-3@inpd.co.za"));

                m.To.Add(new MailboxAddress(PublicVariables.userID));



                m.Subject = "Fourex - " 
                            + PublicVariables.selectedKiosk 
                            + "->" 
                            + "NOTIFICATION," 
                            + PublicVariables.faultType
                            + "," 
                            + PublicVariables.selectedComponent 
                            + "," 
                            + PublicVariables.selectedError 
                            + ","
                            + PublicVariables.userID
                            + "," 
                            + PublicVariables.notes 
                            + "," 
                            + PublicVariables.TimeStamp
                            ;


                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "";

                m.Body = bodyBuilder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
               {
                    var Username = "fourex@inpd.co.za";
                    var Password = "1nn0_Ques2018";

                    await client.ConnectAsync("smtp.inpd.co.za", 587, MailKit.Security.SecureSocketOptions.None);

                    client.AuthenticationMechanisms.Remove("XOAUTH2");


                    await client.AuthenticateAsync(Username, Password)
                        .ConfigureAwait(false);


                    await client.SendAsync(m).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                    PublicVariables.RemedyHours = "00";
                    PublicVariables.RemedyMinutes = "01";

                    PublicVariables.ReturnMessage = "Completed";
                    PublicVariables.notes = "";
                    System.Diagnostics.Debug.WriteLine(m);

                }

            }
            catch (ServiceNotConnectedException e)
            {
                PublicVariables.ReturnMessage = e.Message;
                System.Diagnostics.Debug.WriteLine("ServiceNotConnectedException: " + e.Message);
            }

            catch (Exception e)
            {
                PublicVariables.ReturnMessage = e.Message;
                System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);

            }

        }

        public static String GetTimestamp(DateTime value)
            {
                return value.ToString("yyyyMMddHHmmssffff");
            }



		public List<TicketObject> GetTicketObjects()
		{
			List<TicketObject> ItemsList = new List<TicketObject>();

			var client = new RestClient("http://81.133.234.240/api/console/1");
			var request = new RestRequest(Method.GET);
			IRestResponse response =  client.Execute(request);

			Console.WriteLine("Response: " + response.Content);

			if(!string.IsNullOrEmpty(response.Content))
			{
				var respString = response.Content;
				respString = respString.Replace("NewLine", "]NewLine");

				while (respString.Contains("NewLine"))
				{
					var itemStrt = respString.IndexOf("NewLine")+9;
					respString = respString.Remove(0, itemStrt);
					var itemEnd = respString.IndexOf("]") - 3;
					var item = respString.Substring(0, itemEnd);
					itemEnd = itemEnd + 7;

					respString = respString.Remove(0, itemEnd);
					item = item.Replace(",", "\n");

					if(item.Contains("Blue"))
					{
						item = item.Remove(0,5);
						TicketObject t = new TicketObject(item, "#0054db");
						ItemsList.Add(t);

					}

					else
					{
						item = item.Remove(0,6);
						TicketObject t = new TicketObject(item, "#000000");
						ItemsList.Add(t);

					}


				}

				return ItemsList;
			}

			else
			{
				Toast.MakeText(Application.Context, "Cannot connect, please try again", ToastLength.Long).Show();
				return null;
			}

		}



	}

	public class TicketObject 
	{

		private String mText;
		private String colour;

		public TicketObject(String text, String colour)
		{
			mText = text;
			this.colour = colour;
		}

		public String getText()
		{
			return mText;
		}

		public String getColour()
		{ 
			return colour;
		}



	}
	

	
}


