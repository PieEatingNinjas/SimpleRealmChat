using Realms;
using Realms.Sync;
using ShareRealmThroughQR.Models;
using System;
using System.Threading.Tasks;

namespace ShareRealmThroughQR.Helpers
{
    public static class InvitationHelper
    {
        public static async Task<MyInvitation> CreateInviteAsync(string realmUrl)
        {
            var expiresAt = DateTimeOffset.UtcNow.AddDays(7);
            var token = await User.Current.OfferPermissionsAsync(realmUrl, AccessLevel.Write, expiresAt);
            return CreateInvite(token);
        }

        private static MyInvitation CreateInvite(string token)
        {
            return new MyInvitation(token);
        }

        public static async Task<Realm> AcceptInvitationAsync(MyInvitation invitation)
        {
            var realmUrl = await User.Current.AcceptPermissionOfferAsync(invitation.Token);

            var config = new SyncConfiguration(User.Current, new Uri(realmUrl));
            return await Realm.GetInstanceAsync(config);
        }
    }
}
