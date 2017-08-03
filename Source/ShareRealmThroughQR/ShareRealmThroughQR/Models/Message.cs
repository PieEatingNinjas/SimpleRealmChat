using Realms;

namespace ShareRealmThroughQR.Models
{
    public class Message : RealmObject
    {
        public string Sender { get; set; }
        public string Msg { get; set; }
    }
}
