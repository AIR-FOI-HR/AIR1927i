using KNXcontrol.Models;
using KNXcontrol.Services;
using KNXcontrol.ServicesImplementation;
using KNXcontrol.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KNXcontrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomsOverviewPage : ContentPage
    {
        readonly RoomsViewModel viewModel;
        private readonly RoomsService roomsService = new RoomsService();

        public RoomsOverviewPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new RoomsViewModel();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Rooms?.Count == 0)
                viewModel.IsBusy = true;
        }
        /// <summary>
        /// Opens new modal for creating a room, uses MessagingCenter to retrieve data and pass it to service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddRoom_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewRoomPage(null)));
            MessagingCenter.Subscribe<NewRoomPage, Room>(this, "CREATE", (page, room) =>
            {
                viewModel.Rooms.Add(room);
            });
        }
        /// <summary>
        /// Prompts user to confirm deletion, sends selected room to the service to be deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var id = ((Button)sender).CommandParameter.ToString();

            bool answer = await DisplayAlert("Brisanje prostorije", "Jeste li sigurni da želite obrisati prostoriju?", "Da", "Odustani");

            if (answer)
            {
                var result = await roomsService.DeleteRoom(id);
                if (result)
                {
                    DependencyService.Get<IToastService>().ShowToast("Prostorija je uspješno obrisana!");
                    var deletedItem = viewModel.Rooms.ToList().Find(x => x._id.ToString() == id);
                    viewModel.Rooms.Remove(deletedItem);
                }
            }
        }
        /// <summary>
        /// On doubleTap opens new modal to update room data, uses MessagingCenter to retrieve data and pass to service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnRoomSelection(object sender, EventArgs e)
        {
            var room = ((TappedEventArgs)e).Parameter as Room;

            await Navigation.PushModalAsync(new NavigationPage(new NewRoomPage(room)));
            MessagingCenter.Subscribe<NewRoomPage, Room>(this, "UPDATE", (page, newRoom) =>
            {
                var oldRoom = viewModel.Rooms.FirstOrDefault(x => x._id == room._id);
                int i = viewModel.Rooms.IndexOf(oldRoom);
                viewModel.Rooms[i] = newRoom;
            });
        }
    }
}