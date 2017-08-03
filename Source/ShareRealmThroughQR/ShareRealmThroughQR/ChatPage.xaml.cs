using ShareRealmThroughQR.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShareRealmThroughQR
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        public ChatPage(ChatPageViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}