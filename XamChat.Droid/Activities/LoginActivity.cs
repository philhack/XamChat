using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using XamChat.Core;

namespace XamChat.Droid.Activities
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true)]
    public class LoginActivity : BaseActivity<LoginViewModel> {
        private EditText _username, _password;
        private Button _login;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Login);
            _username = FindViewById<EditText>(Resource.Id.username);
            _password = FindViewById<EditText>(Resource.Id.password);
            _login = FindViewById<Button>(Resource.Id.login);
            _login.Click += OnLogin;
        }

        // overrode OnResume to clear out the two EditText controls so that the values are cleared out if you return to the screen.
        protected override void OnResume() {
            base.OnResume();
            _username.Text = _password.Text = string.Empty;
        }

        async void OnLogin(object sender, EventArgs e) {
            viewModel.Username = _username.Text;
            viewModel.Password = _password.Text;

            try {
                await viewModel.Login();
                // TODO: navigate to a new activity
            }
            catch (Exception ex) {
                DisplayError(ex);
            }
        }
    }
}