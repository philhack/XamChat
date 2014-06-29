using System;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace XamChat.Core {
	public class FakeWebService : IWebService {
		public int SleepDuration { get; set; }

		public FakeWebService() {
			SleepDuration = 1;
		}

		private Task Sleep() {
			return Task.Delay(SleepDuration);
		}

		public async Task<User> Login(string username, string password) {
			await Sleep();

			return new User() {
				Id = 1,
				Username = username,
				Password = password
			};
		}

		public async Task<User> Register(User user) {
			await Sleep();
			return user;
		}

		public async Task<User[]> GetFriends(int userId) {
			await Sleep();

			return new [] {
				new User{ Id = 1, Username = "bobama" },
				new User{ Id = 2, Username = "bobloblaw" },
				new User{ Id = 3, Username = "gmichael" }
			};
		}

		public async Task<User> AddFriend(int userId, string username) {
			await Sleep();

			return new User { Id = 4, Username = username };
		}

		public async Task<Conversation[]> GetConversations(int userId) {
			await Sleep();

			return new [] {
				new Conversation{ Id = 1, UserId = 2 },
				new Conversation{ Id = 1, UserId = 3 },
				new Conversation{ Id = 1, UserId = 4 }
			};
		}

		public async Task<Message[]> GetMessages(int conversationId) {
			await Sleep();

			return new [] {
				new Message {
					Id = 1,
					ConversationId = conversationId,
					UserId = 2,
					Text = "Hey"
				},
				new Message {
					Id = 2,
					ConversationId = conversationId,
					UserId = 1,
					Text = "What's up?"
				},
				new Message {
					Id = 3,
					ConversationId = conversationId,
					UserId = 2,
					Text = "Have you seen that new movie?"
				},
				new Message {
					Id = 4,
					ConversationId = conversationId,
					UserId = 1,
					Text = "Ya, it's fantastic!"
				} 
			};
		}

		public async Task<Message> SendMessage(Message message) {
			await Sleep();

			return message;
		}
	}
}

