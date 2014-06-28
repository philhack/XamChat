using System;
using System.IO;
using XamChat.Core;

namespace XamChat.Core {
	public interface ISettings {
		User User { get; set; }

		void Save();
	}
}

