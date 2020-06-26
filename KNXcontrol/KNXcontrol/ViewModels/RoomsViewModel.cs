using KNXcontrol.Models;
using KNXcontrol.ServicesImplementation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KNXcontrol.ViewModels
{    /// <summary>
     /// Rooms viewModel class - loads rooms from the database to be displayed and managed in the app
     /// inherits BaseViewModel
     /// </summary>
    public class RoomsViewModel : BaseViewModel
    {
        public ObservableCollection<Room> Rooms { get; set; }
        public Command LoadRoomsCommand { get; set; }
        private readonly RoomsService roomsService = new RoomsService();

        public RoomsViewModel()
        {
            Rooms = new ObservableCollection<Room>();
            LoadRoomsCommand = new Command(async () => await ExecuteLoadRoomsCommand());
        }
        /// <summary>
        /// Command for retrieving rooms from the database - implementation
        /// </summary>
        /// <returns></returns>
        async Task ExecuteLoadRoomsCommand()
        {
            IsBusy = true;

            try
            {
                Rooms.Clear();
                var rooms = await roomsService.RoomsOverview();
                foreach (var room in rooms)
                {
                    Rooms.Add(room);
                }
            }
            catch
            {
                Rooms = null;
            }
            finally
            {
                IsBusy = false;
            }
        }      
    }
}
