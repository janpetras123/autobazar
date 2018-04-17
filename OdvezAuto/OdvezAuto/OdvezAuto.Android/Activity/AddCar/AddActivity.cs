using System;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using OdvezAuto.Droid.Activity.Common;
using OdvezAuto.Droid.Helpers;
using OdvezAuto.Entities;
using OdvezAuto.Server;

namespace OdvezAuto.Droid.Activity.AddCar
{
    [Activity]
    class AddActivity : Android.App.Activity
    {
        private Button _backButton;
        private EditText _brandAdd;
        private EditText _typeAdd;
        private Spinner _stateAdd;
        private EditText _kilometersAdd;
        private EditText _powerAdd;
        private EditText _torqueAdd;
        private Button _pictureLoadButton;
        private ImageView _loadedPicture;
        private EditText _priceAdd;
        private Button _sellButton;
        private EditText _imageUrlEditText;
        private string _imageUrl = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Add);

            _backButton = FindViewById<Button>(Resource.Id.BackButton);
            _brandAdd = FindViewById<EditText>(Resource.Id.BrandAdd);
            _typeAdd = FindViewById<EditText>(Resource.Id.TypeAdd);
            _stateAdd = FindViewById<Spinner>(Resource.Id.StateAdd);
            _kilometersAdd = FindViewById<EditText>(Resource.Id.KilometersAdd);
            _powerAdd = FindViewById<EditText>(Resource.Id.PowerAdd);
            _torqueAdd = FindViewById<EditText>(Resource.Id.TorqueAdd);
            _priceAdd = FindViewById<EditText>(Resource.Id.PriceAdd);
            _sellButton = FindViewById<Button>(Resource.Id.SellButton);
            _imageUrlEditText = FindViewById<EditText>(Resource.Id.PictureUrlEditText);

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,
                Android.Resource.Layout.SimpleSpinnerItem, CarStateManager.GetItems());
            _stateAdd.Adapter = adapter;

            _pictureLoadButton = FindViewById<Button>(Resource.Id.PictureLoadButton);
            _loadedPicture = FindViewById<ImageView>(Resource.Id.LoadedPictureImageView);

            _backButton.Click += (sender, args) =>
            {
                Finish();
            };

            _pictureLoadButton.Click += async (sender, args) =>
            {
                _imageUrlEditText.Enabled = false;
                _loadedPicture.Enabled = false;
                _imageUrl = "";
                _loadedPicture.SetImageBitmap(null);
                Bitmap bmp = null;

                await Task.Run(() => { bmp = BitmapDownloader.GetImageBitmapFromUrl(_imageUrlEditText.Text); });
                
                _loadedPicture.SetImageBitmap(bmp);
                _imageUrl = _imageUrlEditText.Text;
                _imageUrlEditText.Enabled = true;
                _loadedPicture.Enabled = true;
            };

            _sellButton.Click += async (sender, args) =>
            {
                bool a, b, c;
                a = int.TryParse(_kilometersAdd.Text, out int kilometerss);
                b = int.TryParse(_powerAdd.Text, out int powesr);


                if (int.TryParse(_kilometersAdd.Text, out int kilometers) && int.TryParse(_powerAdd.Text, out int power) && int.TryParse(_torqueAdd.Text, out int torque) && float.TryParse(_priceAdd.Text, out float price))
                {
                    Car newCar = new Car();
                    newCar.Brand = _brandAdd.Text;
                    newCar.Type = _typeAdd.Text;
                    newCar.State = CarStateManager.ConvertCarStateToDatabaseValue(_stateAdd.SelectedItem.ToString());
                    newCar.Kilometers = kilometers;
                    newCar.Power = power;
                    newCar.Torque = torque;
                    newCar.Price = price;
                    newCar.Picture = _imageUrl;
                    try
                    {
                        await HttpManager.HttpPost(newCar, HttpManager.GetHostAddress() + "/cars");

                        DialogProvider.ShowOkDialogWithoutAction(this, "You sell your car to our bazar!", "Congratulation!", (o, eventArgs) => { Finish(); });
                    }
                    catch (WebException e)
                    {
                        DialogProvider.ShowOkDialogWithoutAction(this, "Connection lost!", "Connection", null);
                    }
                }
                else
                {
                    DialogProvider.ShowOkDialogWithoutAction(this, "Some informations are not correct", "Error", null);
                }
            };
        }
    }
}