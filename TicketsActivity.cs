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
	public class TicketsActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Tickets);


			ProgressDialog p = new ProgressDialog(this);
			p.SetMessage("Loading Alerts...");
			p.SetProgressStyle(ProgressDialogStyle.Spinner);
			p.Show();
			

			var m = new Methods();
			List<TicketObject> list = m.GetTicketObjects();

			ListView listView = FindViewById<ListView>(Resource.Id.tickets_listView);

			TicketsAdapter adapter = new TicketsAdapter(this, list);

			listView.Adapter = adapter;
			
			p.Dismiss();


		}
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.menu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			int id = item.ItemId;

			if (id == Resource.Id.nav_home)
			{
				var intent = new Intent(this, typeof(WelcomeActivity));
				StartActivity(intent);
				return true;
			}

			return base.OnOptionsItemSelected(item);
		}
	}
}