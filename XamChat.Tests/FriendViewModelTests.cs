using System;
using NUnit.Framework;
using XamChat.Core;

namespace XamChat.Tests {
	[TestFixture]
	public class FriendViewModelTests {
		ISettings settings;
		FriendViewModel friendViewModel;

		[SetUp]
		public void SetUp() {
			Test.SetUp();

			settings = ServiceContainer.Resolve<ISettings>();
			friendViewModel = new FriendViewModel();
		}

		[Test]
		public void GetFriendsSuccessfully() {
			LoginTestHelper.SimulateLogin(settings);

			friendViewModel.GetFriends().Wait();

			Assert.That(friendViewModel.Friends, Is.Not.Empty);
		}

		[Test]
		public void GetFriendsWithoutLogin() {
			Assert.Throws<AggregateException>(() => friendViewModel.GetFriends().Wait());
		}

		[Test]
		public void AddFriendSuccessfully() {
			LoginTestHelper.SimulateLogin(settings);

			friendViewModel.Username = "myfriend";

			friendViewModel.AddFriend().Wait();

			Assert.That(friendViewModel.Friends, Has.Some.With.Property("Username").EqualTo(friendViewModel.Username));
		}
	}
}