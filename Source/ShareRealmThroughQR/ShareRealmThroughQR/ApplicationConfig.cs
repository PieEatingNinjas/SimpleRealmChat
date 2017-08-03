namespace ShareRealmThroughQR
{
    public static class ApplicationConfig
    {
        public static readonly string OBJECT_SERVER_IP = "00.00.00.00:9080";
        public static readonly string AUTH_URL = $"http://{OBJECT_SERVER_IP}";
        public static readonly string REALM_URL = $"realm://{OBJECT_SERVER_IP}/~/chat";
    }
}
