using Android.App;
using Android.OS;
using Android.Widget;
using OdvezAuto.Droid.Activity.Common;
using OdvezAuto.Entities;
using OdvezAuto.Server;
using ListActivity = OdvezAuto.Droid.Activity.ListOfCars.ListActivity;

namespace OdvezAuto.Droid.Activity.Login
{
    [Activity(MainLauncher = true)]
    public class LoginActivity : Android.App.Activity
    {
        private EditText _username;
        private EditText _password;
        private Button _loginButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LoginScreen);

            _username = FindViewById<EditText>(Resource.Id.Username);
            _password = FindViewById<EditText>(Resource.Id.Password);
            _loginButton = FindViewById<Button>(Resource.Id.LoginButton);

            _loginButton.Click += (sender, args) =>
            {
                if (_username.Text == "FerkoUzasny" && _password.Text == "FerkoUzasny")
                {
                    StartActivity(typeof(ListActivity));
                }
                else
                {
                    DialogProvider.ShowOkDialogWithoutAction(this, "Username or password is incorrect!", "Error", null);
                }
            };
        }
    }
}