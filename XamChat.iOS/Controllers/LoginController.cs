using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using XamChat.Core;

namespace XamChat.iOS {
	public partial class LoginController : UIViewController {
		readonly LoginViewModel loginViewModel = ServiceContainer.Resolve<LoginViewModel>();

		public LoginController(IntPtr handle)
			: base(handle) {
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();

			login.TouchUpInside += async(sender, e) => {
				loginViewModel.Username = username.Text;
				loginViewModel.Password = password.Text;

				try {
					await loginViewModel.Login();

					PerformSegue("OnLogin", this);	// navigates to the next screen via a Seque (the ConversationsController)
				} catch (Exception ex) {
					new UIAlertView("Oops!", ex.Message, null, "Ok").Show();
				}
			};
		}

		public override void ViewWillAppear(bool animated) {
			base.ViewWillAppear(animated);

			loginViewModel.IsBusyChanged += OnIsBusyChanged;
		}

		public override void ViewWillDisappear(bool animated) {
			base.ViewWillDisappear(animated);

			loginViewModel.IsBusyChanged -= OnIsBusyChanged;
		}

		private void OnIsBusyChanged(object sender, EventArgs e) {
			username.Enabled =
				password.Enabled =
					login.Enabled = 
						indicator.Hidden = !loginViewModel.IsBusy;
		}

	}
}
