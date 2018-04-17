using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Widget;
using OdvezAuto.Droid.Activity.AddCar;
using OdvezAuto.Droid.Activity.Common;
using OdvezAuto.Entities;
using OdvezAuto.Server;


namespace OdvezAuto.Droid.Activity.ListOfCars
{
    [Activity]
    class ListActivity : Android.App.Activity
    {
        private ListView _carList;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.List);

            Button addCarButton = FindViewById<Button>(Resource.Id.AddCarButton);
            Button logOutButton = FindViewById<Button>(Resource.Id.LogOutButton);

            addCarButton.Click += (sender, args) =>
            {
                StartActivity(typeof(AddActivity));
            };

            logOutButton.Click += (sender, args) =>
            {
                Finish(); 
            };

        }
        

        protected override async void OnResume()
        {
            base.OnResume();

            await RefreshList();
        }

        private async Task RefreshList(bool showPopup = true)
        {
            try
            {
                IEnumerable<Car> cars =
                    await HttpManager.HttpGet<IEnumerable<Car>>(HttpManager.GetHostAddress() + "/cars");
                CarAdapter adapter = new CarAdapter();
                _carList = FindViewById<ListView>(Resource.Id.CarList);
                _carList.Adapter = adapter;
                adapter.AddItems(cars);
                adapter.NotifyDataSetChanged();
            }
            catch (WebException e)
            {
                if (showPopup)
                {
                    DialogProvider.ShowOkDialogWithoutAction(this, "Connection lost!", "Connection", null);
                }

                await Task.Run(() => { Thread.Sleep(1000); });
                await RefreshList(false);
            }
        }
    }
}