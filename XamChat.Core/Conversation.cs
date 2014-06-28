using System;
using System.Threading.Tasks;
using System.Configuration;

namespace XamChat.Core {
	public class Conversation {
		public int Id { get; set; }

		public int UserId { get; set; }

		public string Username { get; set; }
	}
}