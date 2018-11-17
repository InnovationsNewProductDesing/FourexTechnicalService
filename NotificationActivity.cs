using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Text.RegularExpressions;

namespace Fourex.Droid
{
    [Activity(Label = "NotificationActivity")]
    public class NotificationActivity : Activity
    {
        

        int component;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "notification" layout resource
            SetContentView(Resource.Layout.Notification);
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);



            Spinner kioskSpinner;
            EditText notesEtxt = FindViewById<EditText>(Resource.Id.notificationNotesEtxt);
            RadioButton customerFaultButton = FindViewById<RadioButton>(Resource.Id.customerFaultCheckBox);
            RadioButton detectorFaultButton = FindViewById<RadioButton>(Resource.Id.detectorFaultCheckBox);

            customerFaultButton.Click += CustomerFaultButton_Click;
            detectorFaultButton.Click += DetectorFaultButton_Click;

            //Kiosk Spinner populate and handle
            kioskSpinner = FindViewById<Spinner>(Resource.Id.notificationKioskIdSpinner);
            var kioskList = Resources.GetStringArray(Resource.Array.kiosk_list);
            var kioskAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, kioskList);
            kioskSpinner.Adapter = kioskAdapter;
            kioskSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>

            {
                PublicVariables.selectedKiosk = kioskList[e.Position];
            };


                        Button submitBtn = FindViewById<Button>(Resource.Id.notificationSubmitButton);

            notesEtxt.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                var notes = e.Text.ToString();
                Regex rgx = new Regex("[^a-zA-Z0-9 ]");
                notes = rgx.Replace(notes, "");
                PublicVariables.notes = notes;
            };
            submitBtn.Click += async delegate {

                if(!customerFaultButton.Checked&& !detectorFaultButton.Checked)
                {
                    Toast.MakeText(this, "Please select a fault type", ToastLength.Long).Show();

                }

                else if(String.IsNullOrEmpty(PublicVariables.selectedKiosk))
                {
                    Toast.MakeText(this, "Please select a kiosk", ToastLength.Long).Show();

                }
               else if(detectorFaultButton.Checked && String.IsNullOrWhiteSpace(PublicVariables.selectedComponent))
                {
                    Toast.MakeText(this, "Please select a component", ToastLength.Long).Show();
                }
                

                else if(String.IsNullOrEmpty(PublicVariables.selectedError))
                {
                    Toast.MakeText(this, "Please select an error", ToastLength.Long).Show();

                }

               else if (String.IsNullOrWhiteSpace(PublicVariables.notes))
                {
                    Toast.MakeText(this, "Please enter a note, only alphanumeric characters accepted", ToastLength.Long).Show();

                }

                else
                {
                    ProgressDialog progressDialog = new ProgressDialog(this);
                    progressDialog.SetMessage("Please wait...");
                    progressDialog.Show();

                    PublicVariables.TimeStamp = Methods.GetTimestamp(DateTime.Now);

                    await Methods.NotificationSubmit();




                    progressDialog.Dispose();

                    Toast.MakeText(this, PublicVariables.ReturnMessage, ToastLength.Long).Show();

                    base.Finish();
                }
            };
        }


        void CustomerFaultButton_Click(object sender1, EventArgs e1)
        {
            PublicVariables.faultType = "Customer Fault";
            PublicVariables.selectedComponent = "";

            TextView compnentTxt = FindViewById<TextView>(Resource.Id.componentTxt);
            Spinner componentSpinner = FindViewById<Spinner>(Resource.Id.notificationComponentSpinner);
            Spinner errorSpinner = FindViewById<Spinner>(Resource.Id.notificationErrorSpinner);

            compnentTxt.Visibility = ViewStates.Gone;
            componentSpinner.Visibility = ViewStates.Gone;


            errorSpinner = FindViewById<Spinner>(Resource.Id.notificationErrorSpinner);
            var customerErrorList = Resources.GetStringArray(Resource.Array.customer_errors_list);
            var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, customerErrorList);
            errorSpinner.Adapter = errorAdapter;
            errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>

            {
                PublicVariables.selectedError = customerErrorList[e.Position];
            };
        }



        void DetectorFaultButton_Click(object sender1, EventArgs e1)
        {
            PublicVariables.faultType = "Detector Fault";

            TextView compnentTxt = FindViewById<TextView>(Resource.Id.componentTxt);
            Spinner componentSpinner = FindViewById<Spinner>(Resource.Id.notificationComponentSpinner);
            Spinner errorSpinner = FindViewById<Spinner>(Resource.Id.notificationErrorSpinner);


            compnentTxt.Visibility = ViewStates.Visible;
            componentSpinner.Visibility = ViewStates.Visible;


            //Component Spinner populate and handle
            componentSpinner = FindViewById<Spinner>(Resource.Id.notificationComponentSpinner);
            var ComponentList = Resources.GetStringArray(Resource.Array.detector_components_list);

            var componentAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, ComponentList);
            componentSpinner.Adapter = componentAdapter;
            componentSpinner.SetSelection(0);

            componentSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
            {
                component = e.Position;
                System.Diagnostics.Debug.WriteLine(component);
                PublicVariables.selectedComponent = ComponentList[e.Position];

                PopulateErrorspinner(component);
            };
        }

        private void PopulateErrorspinner(int component)
        {
            //Error Spinner populate and handle
            var errorSpinner = new Spinner (this);
            errorSpinner = FindViewById<Spinner>(Resource.Id.notificationErrorSpinner);
            var errorList0 = Resources.GetStringArray(Resource.Array.coin_dispensors_errors_list);
            var errorList1 = Resources.GetStringArray(Resource.Array.note_dispensors_errors_list);
            var errorList2 = Resources.GetStringArray(Resource.Array.bill_validator_errors_list);
            var errorList3 = Resources.GetStringArray(Resource.Array.displays_errors_list);
            var errorList4 = Resources.GetStringArray(Resource.Array.inside_kiosk_errors_list);
            var errorList5 = Resources.GetStringArray(Resource.Array.hopper_errors_list);
            var errorList6 = Resources.GetStringArray(Resource.Array.PC_Setup_errors_list);
            var errorList7 = Resources.GetStringArray(Resource.Array.NA_errors_list);

            if (component == 0)
            {

            }

            if (component == 1)
            {
                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem,errorList0);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {

                    PublicVariables.selectedError = errorList0[e.Position];
                };
            }


            if (component == 2)
            {

                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList1);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    

                    PublicVariables.selectedError = errorList1[e.Position];
                };
            }

            if (component == 3)
            {

                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList2);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    

                    PublicVariables.selectedError = errorList2[e.Position];
                };
            }

            if (component == 4)
            {

                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList3);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    

                    PublicVariables.selectedError = errorList3[e.Position];
                };
            }

            if (component == 5)
            {
                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList4);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                   

                    PublicVariables.selectedError = errorList4[e.Position];
                };
            }

            if (component == 6)
            {

                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList5);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    
                    PublicVariables.selectedError = errorList5[e.Position];
                };
            }

            if (component == 7)
            {

                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList6); 
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {

                    PublicVariables.selectedError = errorList6[e.Position];
                };
            }

            if (component == 8)
            {

                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList7);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {

                    PublicVariables.selectedError = errorList7[e.Position];
                };
            }





        }

    }
}
