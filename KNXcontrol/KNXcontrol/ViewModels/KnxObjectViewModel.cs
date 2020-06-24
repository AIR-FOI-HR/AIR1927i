using KNXcontrol.Models;
using KNXcontrol.ServicesImplementation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KNXcontrol.ViewModels
{
    class KnxObjectViewModel : BaseViewModel
    {
        public ObservableCollection<KnxObject> KnxObjects { get; set; }
        public Command LoadKnxObjectsCommand { get; set; }
        private readonly KnxObjectsService roomsService = new KnxObjectsService();

        public KnxObjectViewModel()
        {
            KnxObjects = new ObservableCollection<KnxObject>();
            LoadKnxObjectsCommand = new Command(async () => await ExecuteLoadKnxObjectsCommand());
        }

        async Task ExecuteLoadKnxObjectsCommand()
        {
            IsBusy = true;

            try
            {
                KnxObjects.Clear();
                var rooms = await roomsService.KnxObjectsOverview();
                foreach (var room in rooms)
                {
                    KnxObjects.Add(room);
                }
            }
            catch
            {
                KnxObjects = null;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
