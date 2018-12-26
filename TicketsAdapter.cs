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
	class TicketsAdapter : BaseAdapter<TicketObject>
	{
		private List<TicketObject> mItems;
		private Context mContext;

		public override int Count
		{
			get { return mItems.Count; }
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return null;
		}

		public TicketObject GetTicketObject(int position)
		{
			return mItems[position];
		}


		public TicketsAdapter(Context context,List<TicketObject>items)
		{
			mItems = items;
			mContext = context;

		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override TicketObject this[int position]
		{
			get { return mItems[position]; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;
			if (row == null)
			{
				row = LayoutInflater.From(mContext).Inflate(Resource.Layout.TicketItem, null, false);
			}

			TextView txtView = row.FindViewById<TextView>(Resource.Id.ticketItemTextView);
			TicketObject t = (TicketObject) GetTicketObject(position);
			txtView.SetTextColor(Android.Graphics.Color.ParseColor(t.getColour()));
			txtView.Text = t.getText();

			return row;
		}

		
	}
}