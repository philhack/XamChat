using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Media.Audiofx;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamChat.Core;
using Message = XamChat.Core.Message;

namespace XamChat.Droid.Activities {
    [Activity(Label = "Messages")]
    public class MessagesActivity : Activity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
        }


        private class Adapter : BaseAdapter<Message> {
            private readonly MessageViewModel _messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
            private readonly ISettings _settings = ServiceContainer.Resolve<ISettings>();
            private readonly LayoutInflater _inflater;
            private const int MyMessageType = 0;
            private const int TheirMessageType = 1;

            public Adapter(Context context) {
                _inflater = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
            }


            public override long GetItemId(int position) {
                return _messageViewModel.Messages[position].Id;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var message = this[position];
                int type = GetItemViewType(position);

                if (convertView == null)
                {
                    if (type == MyMessageType)
                    {
                        convertView = _inflater.Inflate(Resource.Layout.MyMessageListItem, null);
                    }
                    else
                    {
                        convertView = _inflater.Inflate(Resource.Layout.TheirMessageListItem, null);
                    }
                }

                TextView messageText, dateText;
                if (type == MyMessageType)
                {
                    messageText = convertView.FindViewById<TextView>(Resource.Id.myMessageText);
                    dateText = convertView.FindViewById<TextView>(Resource.Id.myMessageDate);
                }
                else
                {
                    messageText = convertView.FindViewById<TextView>(Resource.Id.theirMessageText);
                    dateText = convertView.FindViewById<TextView>(Resource.Id.theirMessageDate);
                }

                messageText.Text = message.Text;
                dateText.Text = message.Date.ToString("MM/DD/yy HH:mm");
                return convertView;
            }

            public override int Count {
                get { return _messageViewModel.Messages == null ? 0 : _messageViewModel.Messages.Length; }
            }

            public override Message this[int index] {
                get { return _messageViewModel.Messages[index]; }
            }

            public override int ViewTypeCount {
                get { return 2; }
            }

            public override int GetItemViewType(int position) {
                var message = this[position];
                return message.UserId == _settings.User.Id ? MyMessageType : TheirMessageType;
            }
        }
    }
}