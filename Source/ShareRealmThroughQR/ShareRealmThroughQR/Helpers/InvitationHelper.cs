using Realms;
using Realms.Sync;
using ShareRealmThroughQR.Extensions;
using ShareRealmThroughQR.Models;
using System;
using System.Threading.Tasks;

namespace ShareRealmThroughQR.Helpers
{
    public static class InvitationHelper
    {
        public static Task<MyInvitation> CreateInviteAsync(string realmUrl)
        {
            var tcs = new TaskCompletionSource<MyInvitation>();

            var permissionOffer = new PermissionOffer(realmUrl,
                                         mayRead: true,
                                         mayWrite: true,
                                         mayManage: false,
                                         expiresAt: DateTimeOffset.UtcNow.AddDays(7));

            var managementRealm = User.Current.GetManagementRealm();

            managementRealm.Write(() =>
            {
                managementRealm.Add(permissionOffer);
            });

            permissionOffer.WhenPropertyChanged(nameof(PermissionOffer.Status),
                (po) => po.Status == ManagementObjectStatus.Success,
                (po) =>
                {
                    tcs.TrySetResult(CreateInvite(po));
                }
            );

            return tcs.Task;
        }

        private static MyInvitation CreateInvite(PermissionOffer po)
        {
            return new MyInvitation(po.Token);
        }

        public static Task<Realm> AcceptInvitationAsync(MyInvitation invitation)
        {
            var tcs = new TaskCompletionSource<Realm>();

            var offerResponse = new PermissionOfferResponse(invitation.Token);
            var managementRealm = User.Current.GetManagementRealm();

            managementRealm.Write(() =>
            {
                managementRealm.Add(offerResponse);
            });

            offerResponse.WhenPropertyChanged(
                nameof(PermissionOfferResponse.Status),
                (por) => por.Status == ManagementObjectStatus.Success,
                (por) =>
                {
                    if (por.RealmUrl != null)
                    {
                        var configuration = new SyncConfiguration(User.Current, new Uri(por.RealmUrl));
                        tcs.TrySetResult(Realm.GetInstance(configuration));
                    }
                });

            return tcs.Task;
        }
    }
}
