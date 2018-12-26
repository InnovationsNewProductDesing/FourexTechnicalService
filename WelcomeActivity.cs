using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

using Android.Views;

namespace Fourex.Droid
{
    [Activity(Label = "Fourex Technicians", MainLauncher = true, Icon = "@mipmap/icon")]
    public class WelcomeActivity : AppCompatActivity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Welcome);

           


            var UserID = Application.Context.GetSharedPreferences("Users", Android.Content.FileCreationMode.Private);
            PublicVariables.userID = UserID.GetString("ID", null);

            Button createFualtBtn = FindViewById<Button>(Resource.Id.FaultButton);
            Button createReportBtn = FindViewById<Button>(Resource.Id.ReportButton);
            EditText techID = FindViewById<EditText>(Resource.Id.TechIdEtxt);

            techID.Text = PublicVariables.userID;


            techID.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                PublicVariables.userID = e.Text.ToString();
            };

            techID.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
        {
            PublicVariables.userID = e.Text.ToString();
        };




            createReportBtn.Click += delegate
            {

                if (string.IsNullOrWhiteSpace(PublicVariables.userID) || !PublicVariables.userID.Contains(".") || !PublicVariables.userID.Contains("@"))
                {
                    Toast.MakeText(this, "Please enter a valid email address", ToastLength.Long).Show();
                }

                else
                {

                    UserID = Application.Context.GetSharedPreferences("Users", Android.Content.FileCreationMode.Private);
                    var UserEdit = UserID.Edit();
                    UserEdit.PutString("ID", PublicVariables.userID);
                    UserEdit.Commit();



                    var intent = new Intent(this, typeof(RemedyActivity));
                    StartActivity(intent);
                }
            };



            createFualtBtn.Click += delegate
            {
                if (string.IsNullOrWhiteSpace(PublicVariables.userID) || !PublicVariables.userID.Contains(".") || !PublicVariables.userID.Contains("@"))
                {
                    Toast.MakeText(this, "Please enter a valid email address", ToastLength.Long).Show();
                }

                else
                {
                    UserID = Application.Context.GetSharedPreferences("Users", Android.Content.FileCreationMode.Private);
                    var UserEdit = UserID.Edit();
                    UserEdit.PutString("ID", PublicVariables.userID);
                    UserEdit.Commit();

                    var intent = new Intent(this, typeof(NotificationActivity));
                    StartActivity(intent);
                }
            };



        }

	
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.menu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			int id = item.ItemId;
			
			if (id == Resource.Id.nav_ticket)
			{
				var intent = new Intent(this, typeof(TicketsActivity));
				StartActivity(intent);
				return true;
			}

			return base.OnOptionsItemSelected(item);
		}

	}
}
