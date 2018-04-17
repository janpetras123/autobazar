using System;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using OdvezAuto.Droid.Activity.Common;
using OdvezAuto.Droid.Helpers;
using OdvezAuto.Entities;
using OdvezAuto.Server;

namespace OdvezAuto.Droid.Activity.EditCar
{
    [Activity]
    class EditActivity : Android.App.Activity
    {
        private Button _backButton;
        private EditText _brandEdit;
        private EditText _typeEdit;
        private Spinner _stateEdit;
        private EditText _kilometersEdit;
        private EditText _powerEdit;
        private EditText _torqueEdit;
        private EditText _priceEdit;
        private Button _editButton;
        private Button _pictureLoadButton;
        private EditText _imageUrlEditText;
        private ImageView _loadedPicture;

        private static readonly string CarIdExtras = "CarIdExtras";
        private string _imageUrl;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Edit);
            Car car = null;
            try
            {
                car = await HttpManager.HttpGet<Car>(String.Format(HttpManager.GetHostAddress() + "/cars/{0}", Intent.GetLongExtra(CarIdExtras, 0)));
            }
            catch (WebException e)
            {
                DialogProvider.ShowOkDialogWithoutAction(this, "Connection lost!", "Connection", (sender, args) => { Finish(); });
                return;
            }

            

            _backButton = FindViewById<Button>(Resource.Id.BackButton);
            _brandEdit = FindViewById<EditText>(Resource.Id.BrandEdit);
            _typeEdit = FindViewById<EditText>(Resource.Id.TypeEdit);
            _stateEdit = FindViewById<Spinner>(Resource.Id.StateEdit);
            _kilometersEdit = FindViewById<EditText>(Resource.Id.KilometersEdit);
            _powerEdit = FindViewById<EditText>(Resource.Id.PowerEdit);
            _torqueEdit = FindViewById<EditText>(Resource.Id.TorqueEdit);
            _priceEdit = FindViewById<EditText>(Resource.Id.PriceEdit);
            _editButton = FindViewById<Button>(Resource.Id.EditButton);

            _pictureLoadButton = FindViewById<Button>(Resource.Id.PictureLoadButton);
            _imageUrlEditText = FindViewById<EditText>(Resource.Id.PictureUrlEditText);
            _loadedPicture = FindViewById<ImageView>(Resource.Id.LoadedPictureImageView);

            _brandEdit.Text = car.Brand;
            _typeEdit.Text = car.Type;
            _kilometersEdit.Text = car.Kilometers.ToString();
            _powerEdit.Text = car.Power.ToString();
            _torqueEdit.Text = car.Torque.ToString();
            _priceEdit.Text = car.Price.ToString();
            _imageUrlEditText.Text = car.Picture;


            Bitmap curentBmp = null;

            await Task.Run(() => { curentBmp = BitmapDownloader.GetImageBitmapFromUrl(_imageUrlEditText.Text); });

            _loadedPicture.SetImageBitmap(curentBmp);


            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,
                Android.Resource.Layout.SimpleSpinnerItem, CarStateManager.GetItems());
            _stateEdit.Adapter = adapter;

            _stateEdit.SetSelection(CarStateManager.GetItemPossition(car.State));

            _backButton.Click += (sender, args) =>
            {
                Finish(); 
            };

            _pictureLoadButton.Click += async (sender, args) =>
            {
                _imageUrlEditText.Enabled = false;
                _loadedPicture.Enabled = false;
                _loadedPicture.SetImageBitmap(null);
                Bitmap bmp = null;

                await Task.Run(() => { bmp = BitmapDownloader.GetImageBitmapFromUrl( _imageUrlEditText.Text); });

                _loadedPicture.SetImageBitmap(bmp);
                _imageUrl = _imageUrlEditText.Text;
                _imageUrlEditText.Enabled = true;
                _loadedPicture.Enabled = true;
            };

            _editButton.Click += async (sender, args) =>
            {
                if (int.TryParse(_kilometersEdit.Text, out int kilometers) &&
                    int.TryParse(_powerEdit.Text, out int power) &&
                    int.TryParse(_torqueEdit.Text, out int torque) &&
                    float.TryParse(_priceEdit.Text, out float price))
                {
                    Car editCar = new Car();
                    editCar.Brand = _brandEdit.Text;
                    editCar.Type = _typeEdit.Text;
                    editCar.State = CarStateManager.ConvertCarStateToDatabaseValue(_stateEdit.SelectedItem.ToString());
                    editCar.Kilometers = kilometers;
                    editCar.Power = power;
                    editCar.Torque = torque;
                    editCar.Price = price;
                    editCar.Picture = _imageUrl;
                    try
                    {
                        await HttpManager.HttpPut(editCar, HttpManager.GetHostAddress() + "/cars/" + car.Id);
                        Finish();
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

        public static void StartActivity(Context context, long id)
        {
            Intent activity = new Intent(context, typeof(EditActivity));
            activity.PutExtra(CarIdExtras, id);
            context.StartActivity(activity);
        }
    }
}