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

        /*
         what is going on inside the adapter:
            - We subclassed BaseAdapter<Conversation>.
            - We passed in a Context parameter (our activity) so that we can pull out the LayoutInflater. 
              This class enables us to load XML layout resources and inflate them into a view object
            - We implemented GetItemId. This is a general method used to identify rows, so try to return a unique number.
            - We set up GetView, which recycles the convertView variable by only creating a new view if it is null. 
              We also pulled out the text views in our layout to set their text.
            - We overrode Count to return the number of conversations.
            - We implemented an indexer to return a Conversation object for a position.
         */

        class Adapter : BaseAdapter<Conversation>
        {
            private readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
            private readonly LayoutInflater inflater;

            public Adapter(Context context)
            {
                inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            }

            public override long GetItemId(int position)
            {
                return messageViewModel.Conversations[position].Id;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                if (convertView == null)
                {
                    convertView = inflater.Inflate(Resource.Layout.ConversationListItem, null);
                }
                var conversation = this[position];
                var username = convertView.FindViewById<TextView>(Resource.Id.conversationUsername);
                var lastMessage = convertView.FindViewById<TextView>(Resource.Id.conversationLastMessage);
                username.Text = conversation.Username;
                lastMessage.Text = conversation.LastMessage;    // TODO: need to populate last message
                return convertView;
            }

            public override int Count {
                get { return messageViewModel.Conversations == null ? 0 : messageViewModel.Conversations.Length; }
            }

            public override Conversation this[int index]
            {
                get { return messageViewModel.Conversations [index]; }
            }
        }
    }
}