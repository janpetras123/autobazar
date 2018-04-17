using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using OdvezAuto.Droid.Activity.CarDetail;
using OdvezAuto.Droid.Helpers;
using OdvezAuto.Entities;

namespace OdvezAuto.Droid.Activity.ListOfCars
{
    public class CarListRow : LinearLayout
    {
        private readonly ImageView _rowImage;

        public CarListRow(Context context, Car car) : base(context)
        {
            Inflate(context, Resource.Layout.CarListRow, this);

            TextView brandTypeList = FindViewById<TextView>(Resource.Id.BrandTypeList);
            TextView priceList = FindViewById<TextView>(Resource.Id.PriceList);
            _rowImage = FindViewById<ImageView>(Resource.Id.RowImageView);

            brandTypeList.Text = String.Format("{0} {1}", car.Brand, car.Type);
            priceList.Text = String.Format("{0}€", car.Price);
            
            Click += (sender, args) =>
            {
                DetailActivity.StartActivity(context, car.Id);
            };

            LoadImage(car);
        }

        private async Task LoadImage(Car car)
        {
            Bitmap bmp = null;

            await Task.Run(() => { bmp = BitmapDownloader.GetImageBitmapFromUrl(car.Picture); });

            _rowImage.SetImageBitmap(bmp);
        }
    }
}