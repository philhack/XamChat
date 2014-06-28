using System;
using NUnit.Framework;
using XamChat.Core;

namespace XamChat.Tests {
	[TestFixture]
	public class RegisterViewModelTests {
		ISettings settings;
		RegisterViewModel registerViewModel;

		[SetUp]
		public void SetUp() {
			settings = ServiceContainer.Resolve<ISettings>();
			registerViewModel = new RegisterViewModel();
		}

		[Test]
		public void RegisterSuccessfully() {
			registerViewModel.Username = "testuser";
			registerViewModel.Password = "password";
			registerViewModel.ConfirmPassword = "password";

			registerViewModel.Register().Wait();

			Assert.That(settings.User, Is.Not.Null);
		}
	}
}

