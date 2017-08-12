using Realms;
using Realms.Sync;
using ShareRealmThroughQR.Helpers;
using ShareRealmThroughQR.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShareRealmThroughQR.ViewModels
{
    public class MainPageViewModel
    {
        ICommand _InviteCommand;
        public ICommand InviteCommand
        {
            get => _InviteCommand ?? (_InviteCommand = new Command(() => OnInviteCommand()));
        }

        ICommand _JoinCommand;
        public ICommand JoinCommand
        {
            get => _JoinCommand ?? (_JoinCommand = new Command(() => OnJoinCommand()));
        }

        private async Task OnInviteCommand()
        {
            //Login as User1
            await Login("User1", "pwd");

            //create Realm
            var realmConfig = new SyncConfiguration(User.Current, new Uri(ApplicationConfig.REALM_URL));
            var x = Realm.GetInstance(realmConfig);

            MyInvitation invite = await InvitationHelper.CreateInviteAsync(ApplicationConfig.REALM_URL);
            //MyInvitation is an object that holds the token.
            //Wrapped this inside an object so I could add some custom meta data if I would want to

            //Invite page will show the serialized MyInvitation object as a QR Code
            var invitePage = new InvitePage(invite);

            //Quick n dirty navigation
            await Application.Current.MainPage.Navigation.PushModalAsync(invitePage);
            invitePage.Disappearing += (s, e) => 
            {
                //Quick n dirty navigation
                //When closing invite page, we want to open the chat page.
                var configuration = new SyncConfiguration(User.Current, new Uri(ApplicationConfig.REALM_URL));
                var realm = Realm.GetInstance(configuration);
                Application.Current.MainPage.Navigation.PushModalAsync(new ChatPage(new ChatPageViewModel(realm)));
            };  
        }

        private async Task OnJoinCommand()
        {
            //Login as User2
            await Login("User2", "pwd");

            MyInvitation invite = await ScannerHelper.ScanInviteAsync();

            if(invite != null)
            {
                var realm = await InvitationHelper.AcceptInvitationAsync(invite);
                await Application.Current.MainPage.Navigation.PushModalAsync(new ChatPage(new ChatPageViewModel(realm)));
            }
        }

        private async Task Login(string username, string password)
        {
            var credentials = Credentials.UsernamePassword(username, password, createUser: false);

            var authURL = new Uri(ApplicationConfig.AUTH_URL);
            await User.LoginAsync(credentials, authURL);
        }
    }
}
