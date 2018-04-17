using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OdvezAuto.Droid.Activity.EditCar;
using OdvezAuto.Entities;

namespace OdvezAuto.Droid.Activity.ListOfCars
{
    public class CarAdapter : BaseAdapter<Car>
    {
        private readonly List<Car> _items = new List<Car>();

        public CarAdapter()
        {

        }


        public override long GetItemId(int position)
        {
            return _items[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            return new CarListRow(parent.Context, _items[position]);
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override Car this[int position] => _items[position];

        public void AddItems(IEnumerable<Car> cars)
        {
            _items.AddRange(cars);
            NotifyDataSetChanged();
        }
    }
}