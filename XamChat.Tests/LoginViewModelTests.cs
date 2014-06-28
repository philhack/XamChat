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
			Test.SetUp();

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
	}
}