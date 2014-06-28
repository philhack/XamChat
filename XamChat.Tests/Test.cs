using NUnit.Framework;
using System;
using XamChat.Core;

namespace XamChat.Tests {
	[TestFixture()]
	public class Test {

		public static void SetUp() {
			ServiceContainer.Register<IWebService>(() => new FakeWebService { SleepDuration = 0 });
			ServiceContainer.Register<ISettings>(() => new FakeSettings());
		}
	}
}