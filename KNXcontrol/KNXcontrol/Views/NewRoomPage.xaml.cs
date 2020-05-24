using KNXcontrol.Models;
using KNXcontrol.Services;
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
        public bool IsUpdate { get; set; }

        public NewRoomPage(Room room)
        {
            InitializeComponent();

            Room = room ?? new Room();
            IsUpdate = room != null ? true : false;

            BindingContext = this;
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (IsUpdate)
            {
                await roomsService.UpdateRoom(Room);
                DependencyService.Get<IToastService>().ShowToast("Soba je uspješno ažurirana!");
                MessagingCenter.Send(this, "UPDATE", Room);
                await Navigation.PopModalAsync();
            }
            else
            {
                await roomsService.AddRoom(Room);
                DependencyService.Get<IToastService>().ShowToast("Soba je uspješno kreirana!");
                MessagingCenter.Send(this, "CREATE", Room);
                await Navigation.PopModalAsync();
            }
        }
    }
}