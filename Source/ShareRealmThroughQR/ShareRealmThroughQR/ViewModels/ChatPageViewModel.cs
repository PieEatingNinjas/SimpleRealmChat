using Realms;
using Realms.Sync;
using ShareRealmThroughQR.Models;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShareRealmThroughQR.ViewModels
{
    public class ChatPageViewModel
    {
        Realm realm;
        public IQueryable<Message> Messages
        {
            get
            {
                return realm?.All<Message>();
            }
        }
        public ChatPageViewModel(Realm realm)
        {
            this.realm = realm;
        }

        public string Message { get; set; }

        ICommand _PostCommand;
        public ICommand PostCommand
        {
            get => _PostCommand ?? (_PostCommand = new Command(OnPostCommand));
        }

        private void OnPostCommand(object obj)
        {
            realm.Write(() =>
            {
                realm.Add(new Message()
                {
                    Sender = User.Current.Identity,
                    Msg = Message
                });
            });
            Message = null;
        }
    }
}
