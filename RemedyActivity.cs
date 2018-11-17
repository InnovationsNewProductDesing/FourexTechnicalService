using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;
using System.Text.RegularExpressions;

namespace Fourex.Droid
{
    [Activity(Label = "Fourex")]
    public class RemedyActivity : Activity
    {
        Spinner kioskSpinner;
        Spinner compontentSpinner;
        Spinner errorSpinner;
        Spinner remedySpinner;
        int component;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);





            var faultno = Application.Context.GetSharedPreferences("FaultID", Android.Content.FileCreationMode.Private);


            // Set our view from the "remedy" layout resource
            SetContentView(Resource.Layout.Remedy);
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);


            NumberPicker hrPicker = FindViewById<NumberPicker>(Resource.Id.hourNumberPicker);
            NumberPicker minPicker = FindViewById<NumberPicker>(Resource.Id.minuteNumberPicker);

            hrPicker.MinValue = 0;
            hrPicker.MaxValue = 99;
            //hrPicker.SetFormatter(new TwoDigitFormatter());




            hrPicker.ValueChanged += (sender, args) =>
            {
                String value = String.Format("{0:D2}", args.NewVal);
                PublicVariables.RemedyHours = value;
            } ;




            minPicker.MinValue = 0;
            minPicker.MaxValue = 59;
            //minPicker.SetFormatter(new TwoDigitFormatter());

            minPicker.ValueChanged += (sender, args) =>
            {
                String value = String.Format("{0:D2}", args.NewVal);


                PublicVariables.RemedyMinutes = value;
            };



            //Kiosk Spinner populate and handle
            kioskSpinner = FindViewById<Spinner>(Resource.Id.remedyKioskIdSpinner);
            var kioskList = Resources.GetStringArray(Resource.Array.kiosk_list);
            var kioskAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, kioskList);
            kioskSpinner.Adapter = kioskAdapter;
            kioskSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>

            {
                PublicVariables.selectedKiosk = kioskList[e.Position];
            };




            //Component Spinner populate and handle
            compontentSpinner = FindViewById<Spinner>(Resource.Id.remedyComponentSpinner);
            var ComponentList = Resources.GetStringArray(Resource.Array.detector_components_list);
            compontentSpinner.SetSelection(0);

            var componentAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, ComponentList);
            compontentSpinner.Adapter = componentAdapter;

            compontentSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
            {
                component = e.Position;
                System.Diagnostics.Debug.WriteLine(component);
                PublicVariables.selectedComponent = ComponentList[e.Position];

                PopulateErrorspinner(component);
            };





            Button submitBtn = FindViewById<Button>(Resource.Id.remedySubmitButton); 
            EditText notesEtxt = FindViewById<EditText>(Resource.Id.remedyNotesEtxt);


            notesEtxt.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                var notes = e.Text.ToString();

                Regex rgx = new Regex("[^a-zA-Z0-9 ]");
                notes = rgx.Replace(notes, "");
                PublicVariables.notes = notes;


            };

               



            submitBtn.Click += async delegate {

                if (String.IsNullOrEmpty(PublicVariables.selectedKiosk))
                {
                    Toast.MakeText(this, "Please select a kiosk", ToastLength.Long).Show();

                }
                else if ( String.IsNullOrWhiteSpace(PublicVariables.selectedComponent))
                {
                    Toast.MakeText(this, "Please select a component", ToastLength.Long).Show();
                }


                else if (String.IsNullOrEmpty(PublicVariables.selectedError))
                {
                    Toast.MakeText(this, "Please select an error", ToastLength.Long).Show();

                }

                else if (String.IsNullOrEmpty(PublicVariables.selectedRemedy))
                {
                    Toast.MakeText(this, "Please select a remedy", ToastLength.Long).Show();

                }
                else if (PublicVariables.RemedyHours=="0"|| hrPicker.Value==0
                && PublicVariables.RemedyMinutes=="0"||minPicker.Value==0)
                {
                    Toast.MakeText(this, "Please select a duration", ToastLength.Long).Show();

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

                    await Methods.RemedySubmit();

                    progressDialog.Dispose();
                    Toast.MakeText(this, PublicVariables.ReturnMessage, ToastLength.Long).Show();
                    base.Finish();
                }
            };



            }


            void PopulatRemedySpinner(int component)
            {
                //Error Spinner populate and handle
                remedySpinner = FindViewById<Spinner>(Resource.Id.remedyRemedySpinner);
                var remedylist0 = Resources.GetStringArray(Resource.Array.coin_dispenser_remedy_list);
                var remedyList1 = Resources.GetStringArray(Resource.Array.note_dispenser_remedy_list);
                var remedyList2 = Resources.GetStringArray(Resource.Array.bill_validator_remedy_list);
                var remedyList3 = Resources.GetStringArray(Resource.Array.display_remedy_list);
                var remedyList4 = Resources.GetStringArray(Resource.Array.inside_kiosk_remedy_list);
                var remedyList5 = Resources.GetStringArray(Resource.Array.hopper_remedy_list);
                var remedyList6 = Resources.GetStringArray(Resource.Array.PC_Setup_remedy_List);
                var remedyList7 = Resources.GetStringArray(Resource.Array.NA_remedy_list);

            if (component == 0)
            {

            }

                if (component == 1)
                {
                var remedyAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem ,remedylist0);
                    remedySpinner.Adapter = remedyAdapter;

                    remedySpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                    {
                        PublicVariables.selectedRemedy = remedylist0[e.Position];
                    };
                }

                if (component == 2)
                {
                var remedyAdapter = new ArrayAdapter(this,Resource.Layout.SpinnerItem ,remedyList1);
                    remedySpinner.Adapter = remedyAdapter;

                    remedySpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                    {
                        PublicVariables.selectedRemedy = remedyList1[e.Position];
                    };
                }

                if (component == 3)
                {
                var remedyAdapter = new ArrayAdapter(this,Resource.Layout.SpinnerItem, remedyList2);
                    remedySpinner.Adapter = remedyAdapter;

                    remedySpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                    {
                        PublicVariables.selectedRemedy = remedyList2[e.Position];
                    };
                }

                if (component == 4)
                {
                var remedyAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, remedyList3);
                    remedySpinner.Adapter = remedyAdapter;

                    remedySpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                    {
                        PublicVariables.selectedRemedy = remedyList3[e.Position];
                    };
                }

                if (component == 5)
                {
                var remedyAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, remedyList4);
                    remedySpinner.Adapter = remedyAdapter;

                    remedySpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                    {
                        PublicVariables.selectedRemedy = remedyList4[e.Position];
                    };
                }

                if (component == 6)
                {
                var remedyAdapter = new ArrayAdapter(this,Resource.Layout.SpinnerItem, remedyList5);
                    remedySpinner.Adapter = remedyAdapter;

                    remedySpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                    {
                        PublicVariables.selectedRemedy = remedyList5[e.Position];
                    };
                }

                if (component == 7)
                {
                    var remedyAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, remedyList6);
                    remedySpinner.Adapter = remedyAdapter;

                    remedySpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                    {
                        PublicVariables.selectedRemedy = remedyList6[e.Position];
                    };
                }

            if (component == 8)
            {
                var remedyAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, remedyList7);
                remedySpinner.Adapter = remedyAdapter;

                remedySpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    PublicVariables.selectedRemedy = remedyList7[e.Position];
                };
            }






        }

        void PopulateErrorspinner(int component)
        {
            //Error Spinner populate and handle
            errorSpinner = FindViewById<Spinner>(Resource.Id.remedyErrorSpinner);
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
                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList0);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    //error = e.Position;
                    PopulatRemedySpinner(component);

                    PublicVariables.selectedError = errorList0[e.Position];
                };
            }


            if (component == 2)
            {

                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList1);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    //error = e.Position;
                    PopulatRemedySpinner(component);

                    PublicVariables.selectedError = errorList1[e.Position];
                };
            }

            if (component == 3)
            {

                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList2);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    //error = e.Position;
                    PopulatRemedySpinner(component);

                    PublicVariables.selectedError = errorList2[e.Position];
                };
            }

            if (component == 4)
            {

                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList3);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    //error = e.Position;
                    PopulatRemedySpinner(component);

                    PublicVariables.selectedError = errorList3[e.Position];
                };
            }

            if (component == 5)
            {
                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList4);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    //error = e.Position;
                    PopulatRemedySpinner(component);

                    PublicVariables.selectedError = errorList4[e.Position];
                };
            }

            if (component == 6)
            {

                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList5);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    // error = e.Position;
                    PopulatRemedySpinner(component);
                    PublicVariables.selectedError = errorList5[e.Position];
                };
            }

            if (component == 7)
            {

                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList6);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    // error = e.Position;
                    PopulatRemedySpinner(component);
                    PublicVariables.selectedError = errorList6[e.Position];
                };
            }


            if (component == 8)
            {

                var errorAdapter = new ArrayAdapter(this, Resource.Layout.SpinnerItem, errorList7);
                errorSpinner.Adapter = errorAdapter;

                errorSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    // error = e.Position;
                    PopulatRemedySpinner(component);
                    PublicVariables.selectedError = errorList7[e.Position];
                };
            }




        }






            };


    public class TwoDigitFormatter : Java.Lang.Object,NumberPicker.IFormatter

    {
        public string Format(int value)
        {
            return string.Format("{0:D2}", value);
        }
    }






}

