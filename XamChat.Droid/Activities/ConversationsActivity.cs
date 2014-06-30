using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamChat.Core;

namespace XamChat.Droid.Activities {
    [Activity(Label = "Conversations")]
    public class ConversationsActivity : BaseActivity<MessageViewModel> {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Create your application here
        }
    }
}