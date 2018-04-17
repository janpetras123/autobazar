using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OdvezAuto.Droid.Activity.Common;
using OdvezAuto.Droid.Helpers;
using OdvezAuto.Entities;
using OdvezAuto.Server;

namespace OdvezAuto.Droid.Activity.CarDetail
{
    [Activity]
    class ZoomActivity : Android.App.Activity
    {
        private Button _backButton;
        private ImageView _picture;

        private static readonly string CarIdExtras = "CarIdExtras";
        private long _carId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Zoom);

            _carId = Intent.GetLongExtra(CarIdExtras, 0);

            _backButton = FindViewById<Button>(Resource.Id.BackButton);
            _picture = FindViewById<ImageView>(Resource.Id.PictureBig);

            _backButton.Click += (sender, args) =>
            {
                Finish();
            };
        }

        protected override async void OnResume()
        {
            base.OnResume();

            try
            {
                Car car = await HttpManager.HttpGet<Car>(String.Format(HttpManager.GetHostAddress() + "/cars/{0}", _carId));

                Bitmap bmp = null;

                await Task.Run(() => { bmp = BitmapDownloader.GetImageBitmapFromUrl(car.Picture); });

                _picture.SetImageBitmap(bmp);

            }
            catch (WebException e)
            {
                DialogProvider.ShowOkDialogWithoutAction(this, "Connection lost!", "Connection", (sender, args) => { Finish(); });
            }
        }

        public static void StartActivity(Context context, long id)
        {
            Intent activity = new Intent(context, typeof(ZoomActivity));
            activity.PutExtra(CarIdExtras, id);
            context.StartActivity(activity);
        }
    }
}