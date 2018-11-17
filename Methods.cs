using System;
using System.Threading.Tasks;
using MimeKit;
using MailKit;
using System.Collections;
using RestSharp;
using System.Text.RegularExpressions;

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

        public async Task Get(string ext)
        {
            var localArrayList = new ArrayList();

            
                var client = new RestClient("http://localhost:59089/api/" + ext);
                var request = new RestRequest(Method.GET);

            client.ExecuteAsync(request, response =>
            {
                System.Diagnostics.Debug.WriteLine("response: " + response.Content);

                string responseString = response.Content;
                responseString = responseString.Replace("[", "").Replace("{", "").Replace("\"", "").Replace("]", "");


                string[] item = responseString.Split('}');

                while (item != null)
                {
                    localArrayList.Add(item);
                }

            });
            
           

        }
    }
}

