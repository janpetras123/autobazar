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

namespace OdvezAuto.Droid.Activity.Common
{
    class CarStateManager
    {
        public static int ConvertCarStateToDatabaseValue(string state)
        {
            int databaseState = 0;

            if (state == "New")
            {
                databaseState = 1;
            }
            else if (state == "Used")
            {
                databaseState = 2;
            }
            else if (state == "Bad")
            {
                databaseState = 3;
            }
            return databaseState;
        }

        public static string ConvertCarStateFromDatabaseValue(int state)
        {
            string databaseState = "New";

            if (state == 1)
            {
                databaseState = "New";
            }
            else if (state == 2)
            {
                databaseState = "Used";
            }
            else if (state == 3)
            {
                databaseState = "Bad";
            }
            return databaseState;
        }

        public static List<string> GetItems()
        {
            return new List<string>() { "New", "Used", "Bad" };
        }

        public static int GetItemPossition(int carState)
        {
            return GetItems().IndexOf(ConvertCarStateFromDatabaseValue(carState));
        }
    }
}