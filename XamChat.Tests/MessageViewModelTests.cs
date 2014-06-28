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
			BaseTest.SetUp();

			settings = ServiceContainer.Resolve<ISettings>();
			messageViewModel = new MessageViewModel();
		}

		[Test]
		public void GetConversationsSuccessfully() {
			LoginTestHelper.SimulateLogin(settings);

			messageViewModel.GetConversations().Wait();

			Assert.That(messageViewModel.Conversations, Is.Not.Empty);
		}

		[Test]
		public void GetConversationsWithoutLogin() {

			Assert.Throws<AggregateException>(() => messageViewModel.GetConversations().Wait());
		}

		[Test]
		public void GetMessagesSuccessfully() {
			LoginTestHelper.SimulateLogin(settings);

			messageViewModel.Conversation = new Conversation();			

			messageViewModel.GetMessages().Wait();

			Assert.That(messageViewModel.Messages, Is.Not.Empty);
		}

		[Test]
		public void GetMessagesWithoutConversation() {
			Assert.Throws<AggregateException>(() => messageViewModel.GetMessages().Wait());
		}


		[Test]
		public void SendMessage() {
			LoginTestHelper.SimulateLogin(settings);

			messageViewModel.Conversation = new Conversation();
			messageViewModel.Text = "Hello";

			messageViewModel.SendMessage().Wait();

			Assert.That(messageViewModel.Messages, Has.Some.With.Property("Text").EqualTo(messageViewModel.Text));
		}


	}
}

