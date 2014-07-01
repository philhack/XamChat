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
    [Activity(Label = "Friends")]
    public class FriendsActivity : BaseActivity<FriendViewModel> {
        private ListView _listView;
        private Adapter _adapter;

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Friends);
            _listView = FindViewById<ListView>(Resource.Id.friendsList);
            _listView.Adapter = _adapter = new Adapter(this);

        }

        protected async override void OnResume() {
            base.OnResume();
            try {
                await viewModel.GetFriends();
                _adapter.NotifyDataSetInvalidated();
            }
            catch (Exception ex) {
                DisplayError(ex);
            }
        }


        //There is really no difference in this adapter and the previous one we implemented for the conversations screen. We only have to set
        private class Adapter : BaseAdapter<User> {
            private readonly FriendViewModel _friendViewModel = ServiceContainer.Resolve<FriendViewModel>();
            private readonly LayoutInflater _inflater;

            public Adapter(Context context) {
                _inflater = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
            }

            public override long GetItemId(int position) {
                return _friendViewModel.Friends[position].Id;
            }

            public override View GetView(int position, View convertView, ViewGroup parent) {
                if (convertView == null) {
                    convertView = _inflater.Inflate(Resource.Layout.FriendListItem, null);
                }

                var friend = this[position];
                var friendName = convertView.FindViewById<TextView>(Resource.Id.friendName);
                friendName.Text = friend.Username;
                return convertView;
            }

            public override int Count {
                get { return _friendViewModel.Friends == null ? 0 : _friendViewModel.Friends.Length; }
            }

            public override User this[int index] {
                get { return _friendViewModel.Friends[index]; }
            }
        }
    }
}