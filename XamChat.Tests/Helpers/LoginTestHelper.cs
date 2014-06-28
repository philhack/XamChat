using System;
using XamChat.Core;

namespace XamChat.Tests {
	public static class LoginTestHelper {
		public static void SimulateLogin(ISettings settings) {
			settings.User = new User();
		}
	}
}

