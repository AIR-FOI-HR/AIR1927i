using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using KNXcontrol.Services;
using KNXcontrol.Views;

namespace KNXcontrol
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
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
