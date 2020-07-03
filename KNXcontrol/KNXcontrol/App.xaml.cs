using Xamarin.Forms;
using KNXcontrol.Services;
using KNXcontrol.Views;

namespace KNXcontrol
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IToastService>();
            DependencyService.Register<IConnector>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
