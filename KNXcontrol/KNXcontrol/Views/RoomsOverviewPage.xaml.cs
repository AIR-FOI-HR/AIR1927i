using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KNXcontrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomsOverviewPage : ContentPage
    {
        public RoomsOverviewPage()
        {
            InitializeComponent();
        }

        private async void AddRoom_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewRoomPage()));
        }
    }
}