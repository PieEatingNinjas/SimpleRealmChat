using ShareRealmThroughQR.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShareRealmThroughQR
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvitePage : ContentPage
    {
        public InvitePage(MyInvitation invite) 
            : this(Newtonsoft.Json.JsonConvert.SerializeObject(invite))
        { }

        public InvitePage(string content)
        {
            InitializeComponent();
            BarCodeView.BarcodeOptions.Height = 300;
            BarCodeView.BarcodeOptions.Width = 300;
            BarCodeView.BarcodeValue = content;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }
    }
}