using KNXcontrol.ServicesImplementation;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KNXcontrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KnxObjectsOverviewPage : ContentPage
    {
        //readonly KnxObjectViewModel viewModel;
        private readonly KnxObjectsService knxObjectsService = new KnxObjectsService();
        public KnxObjectsOverviewPage()
        {
            InitializeComponent();
        }
    }
}