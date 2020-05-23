using KNXcontrol.Models;
using KNXcontrol.ServicesImplementation;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KNXcontrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRoomPage : ContentPage
    {
        public Room Room { get; set; }
        private readonly RoomsService roomsService = new RoomsService();

        public NewRoomPage()
        {
            InitializeComponent();

            Room = new Room();

            BindingContext = this;
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            roomsService.AddRoom(Room);
            Navigation.PopModalAsync();
        }
    }
}