using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using XamChat.Core;
using MonoTouch.CoreText;

namespace XamChat.iOS {
	public partial class ConversationsController : UITableViewController {
		readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();

		public ConversationsController(IntPtr handle)
			: base(handle) {
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();

			TableView.Source = new TableSource(this);
		}

		public async override void ViewWillAppear(bool animated) {
			base.ViewWillAppear(animated);

			try {
				await messageViewModel.GetConversations();
				TableView.ReloadData();
			} catch (Exception ex) {
				new UIAlertView("Oops!", ex.Message, null, "Ok").Show();
			}
		}

		class TableSource : UITableViewSource {
			const string CellName = "ConversationCell";
			readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
			readonly ConversationsController controller;

			public TableSource(ConversationsController controller) {
				this.controller = controller;
			}

			public override int RowsInSection(UITableView tableView, int section) {
				// return the number of conversations found in the view model
				return messageViewModel.Conversations == null ? 0 : messageViewModel.Conversations.Length;
			}

			#region implemented abstract members of UITableViewSource

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
				var conversation = messageViewModel.Conversations[indexPath.Row];
				var cell = tableView.DequeueReusableCell(CellName);
				if (cell == null) {
					cell = new UITableViewCell(UITableViewCellStyle.Default, CellName);
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;		// visual indicator to the user that they clicked that row
				}
				cell.TextLabel.Text = conversation.Username;
				return cell;
			}

			#endregion
		}
	}
}
