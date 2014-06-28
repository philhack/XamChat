using System;
using NUnit.Framework;
using XamChat.Core;
using System.Runtime.Remoting.Messaging;

namespace XamChat.Tests {
	[TestFixture]
	public class MessageViewModelTests {
		MessageViewModel messageViewModel;
		ISettings settings;

		[SetUp]
		public void SetUp() {
			Test.SetUp();

			settings = ServiceContainer.Resolve<ISettings>();
			messageViewModel = new MessageViewModel();
		}

		[Test]
		public void GetConversationsSuccessfully() {
			settings.User = new User();		// simulating login

			messageViewModel.GetConversations().Wait();

			Assert.That(messageViewModel.Conversations, Is.Not.Empty);
		}
	}
}

