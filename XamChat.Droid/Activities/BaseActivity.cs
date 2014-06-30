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
using XamChat.Core;

namespace XamChat.Droid.Activities {
    /*
        We did several things here to simplify the development of our other activities. First,
        we made this class generic, and made a protected variable named viewModel to store
        a view model of a specific type. Note that we did not use generics on controllers in
        iOS due to platform limitations (see more on Xamarin's documentation website at
        http://docs.xamarin.com/guides/ios/advanced_topics/limitations/).
      
        We also implemented IsBusyChanged, and displayed a simple ProgressBar function
        with the Loading string from the Strings.xml file to indicate network activity.
     */
    [Activity]
    public class BaseActivity<TViewModel> : Activity where TViewModel : BaseViewModel {
        protected readonly TViewModel viewModel;
        protected ProgressDialog progress;

        public BaseActivity() {
            viewModel = ServiceContainer.Resolve(typeof (TViewModel)) as TViewModel;
        }

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            progress = new ProgressDialog(this);
            progress.SetCancelable(false);
            progress.SetTitle(Resource.String.Loading);
        }

        protected override void OnResume() {
            base.OnResume();
            viewModel.IsBusyChanged += OnIsBusyChanged;
        }

        protected override void OnPause() {
            base.OnPause();
            viewModel.IsBusyChanged -= OnIsBusyChanged;
        }

        private void OnIsBusyChanged(object sender, EventArgs e) {
            if (viewModel.IsBusy)
                progress.Show();
            else {
                progress.Hide();
            }
        }

        // This method will display a pop-up dialog indicating that something went wrong.
        // Notice we also used ErrorTitle and the built-in Android resource for an Ok string.
        protected void DisplayError(Exception ex) {
            var error = ex.Message;
            new AlertDialog.Builder(this)
                .SetTitle(Resource.String.ErrorTitle)
                .SetMessage(error)
                .SetPositiveButton(Android.Resource.String.Ok, (IDialogInterfaceOnClickListener) null)
                .Show();
        }
    }
}