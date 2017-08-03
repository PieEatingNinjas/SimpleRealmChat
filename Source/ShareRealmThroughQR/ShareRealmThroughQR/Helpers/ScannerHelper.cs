using Newtonsoft.Json;
using ShareRealmThroughQR.Models;
using System;
using System.Threading.Tasks;
using ZXing.Mobile;

namespace ShareRealmThroughQR.Helpers
{
    public static class ScannerHelper
    {
        public static async Task<MyInvitation> ScanInviteAsync()
        {
            var scanner = new MobileBarcodeScanner();

            var result = await scanner.Scan();

            if (result != null)
            {
                try
                {
                    return JsonConvert.DeserializeObject<MyInvitation>(result.Text);
                }
                catch (Exception)
                {
                    //ToDo
                }
            }
            return null;
        }
    }
}
