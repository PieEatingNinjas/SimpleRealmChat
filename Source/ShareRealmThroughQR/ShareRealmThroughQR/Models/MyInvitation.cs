namespace ShareRealmThroughQR.Models
{
    public class MyInvitation
    {
        public string Token { get; }
        public MyInvitation(string token)
        {
            Token = token;
        }
    }
}
