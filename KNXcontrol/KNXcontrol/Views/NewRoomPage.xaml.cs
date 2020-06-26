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
        /// <summary>
        /// Constructor for managing rooms - gets room if update, else null
        /// </summary>
        /// <param name="knxObject"></param>
        /// <param name="rooms"></param>
        /// <param name="types"></param>
        public NewRoomPage(Room room)
        {
            InitializeComponent();

            Room = room ?? new Room();
            IsUpdate = room != null ? true : false;

            Title = IsUpdate ? "Uredi prostoriju" : "Nova prostorija";

            BindingContext = this;
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            Cancel.Clicked += null;
            Cancel.IsEnabled = false;
            await Navigation.PopModalAsync();
        }
        /// <summary>
        /// Validates user input and based on the IsUpdate flag, updates or creates a new room
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Save_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Room.Name))
                DependencyService.Get<IToastService>().ShowToast("Naziv prostorije je obavezan!");
            else
            {
                Save.Clicked += null;
                Save.IsEnabled = false;
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
}