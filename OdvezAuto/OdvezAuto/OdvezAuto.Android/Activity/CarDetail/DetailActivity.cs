using System;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using OdvezAuto.Droid.Activity.Common;
using OdvezAuto.Droid.Activity.EditCar;
using OdvezAuto.Droid.Helpers;
using OdvezAuto.Entities;
using OdvezAuto.Server;

namespace OdvezAuto.Droid.Activity.CarDetail
{
    [Activity]
    class DetailActivity : Android.App.Activity
    {
        private Button _backButton;
        private Button _editButton;
        private TextView _brandDetail;
        private TextView _typeDetail;
        private TextView _stateDetail;
        private TextView _kilometersDetail;
        private TextView _powerDetail;
        private TextView _torqueDetail;
        private ImageView _pictureZoom;
        private TextView _priceDetail;
        private Button _buyButton;

        private static readonly string CarIdExtras = "CarIdExtras";
        private long _carId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Detail);

            _carId = Intent.GetLongExtra(CarIdExtras, 0);

            _backButton = FindViewById<Button>(Resource.Id.BackButton);
            _editButton = FindViewById<Button>(Resource.Id.EditButton);
            _brandDetail = FindViewById<TextView>(Resource.Id.BrandDetail);
            _typeDetail = FindViewById<TextView>(Resource.Id.TypeDetail);
            _stateDetail = FindViewById<TextView>(Resource.Id.StateDetail);
            _kilometersDetail = FindViewById<TextView>(Resource.Id.KilometersDetail);
            _powerDetail = FindViewById<TextView>(Resource.Id.PowerDetail);
            _torqueDetail = FindViewById<TextView>(Resource.Id.TorqueDetail);
            _pictureZoom = FindViewById<ImageView>(Resource.Id.PictureZoom);
            _priceDetail = FindViewById<TextView>(Resource.Id.PriceDetail);
            _buyButton = FindViewById<Button>(Resource.Id.BuyButton);

            _backButton.Click += (sender, args) =>
            {
                Finish();
            };

            _editButton.Click += (sender, args) =>
            {
                EditActivity.StartActivity(this, _carId);
            };

            _pictureZoom.Click += (sender, args) =>
            {
                ZoomActivity.StartActivity(this, _carId);
            };

            _buyButton.Click += async (sender, args) =>
            {
                try
                {
                    await HttpManager.HttpDelete(HttpManager.GetHostAddress() + "/cars/" + _carId);

                    DialogProvider.ShowOkDialogWithoutAction(this, "You buy this car. Thanks for your purchase!", "Congratulation!", (o, eventArgs) => { Finish(); });
                }
                catch (WebException e)
                {
                    DialogProvider.ShowOkDialogWithoutAction(this, "Connection lost!", "Connection", null);
                }
            };
        }

        protected override async void OnResume()
        {
            base.OnResume();

            try
            {
                Car car = await HttpManager.HttpGet<Car>(String.Format(HttpManager.GetHostAddress() + "/cars/{0}", _carId));

                _brandDetail.Text = car.Brand;
                _typeDetail.Text = car.Type;
                _stateDetail.Text = CarStateManager.ConvertCarStateFromDatabaseValue(car.State);
                _kilometersDetail.Text = String.Format("{0}km", car.Kilometers);
                _powerDetail.Text = String.Format("{0}kW", car.Power);
                _torqueDetail.Text = String.Format("{0}Nm", car.Torque);
                _priceDetail.Text = String.Format("{0}€", car.Price);
                Bitmap bmp = null;

                await Task.Run(() => { bmp = BitmapDownloader.GetImageBitmapFromUrl(car.Picture); });

                _pictureZoom.SetImageBitmap(bmp);

            }
            catch (WebException e)
            {
                DialogProvider.ShowOkDialogWithoutAction(this, "Connection lost!", "Connection", (sender, args) => { Finish(); });
            }
        }

        public static void StartActivity(Context context, long id)
        {
            Intent activity = new Intent(context, typeof(DetailActivity));
            activity.PutExtra(CarIdExtras, id);
            context.StartActivity(activity);
        }
    }
}