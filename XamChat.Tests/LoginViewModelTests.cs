using System;
using XamChat.Core;
using NUnit.Framework;

using System.Configuration;
using System.Runtime.InteropServices;

namespace XamChat.Tests {

	[TestFixture]
	public class LoginViewModelTests {
		LoginViewModel loginViewModel;
		ISettings settings;

		[SetUp]
		public void SetUp() {
			BaseTest.SetUp();

			settings = ServiceContainer.Resolve<ISettings>();
			loginViewModel = new LoginViewModel();
		}

		[Test]
		public void LoginSuccessfully() {
			loginViewModel.Username = "testuser";
			loginViewModel.Password = "password";

			loginViewModel.Login().Wait();

			Assert.That(settings.User, Is.Not.Null);
		}

		[Test]
		public void LoginWithNoUsernameOrPassword() {
			var ex = Assert.Throws<AggregateException>(() => loginViewModel.Login().Wait());
		}

		[Test]
		public void LoginWithNoUsername() {
			loginViewModel.Password = "password";

			Assert.Throws<AggregateException>(() => loginViewModel.Login().Wait());
		}

		[Test]
		public void LoginWithNoPassword() {
			loginViewModel.Username = "testuser";

			Assert.Throws<AggregateException>(() => loginViewModel.Login().Wait());
		}
	}
}