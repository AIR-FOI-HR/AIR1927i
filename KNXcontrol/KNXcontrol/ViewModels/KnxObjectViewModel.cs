using KNXcontrol.Services;
using Model.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KNXcontrol.ViewModels
{
    /// <summary>
    /// KNX objects viewModel class - loads KNX objects from the database to be displayed and managed in the app     
    /// /// inherits BaseViewModel
    /// </summary>
    class KnxObjectViewModel : BaseViewModel
    {
        public ObservableCollection<KnxObject> KnxObjects { get; set; }
        public Command LoadKnxObjectsCommand { get; set; }

        public KnxObjectViewModel()
        {
            KnxObjects = new ObservableCollection<KnxObject>();
            LoadKnxObjectsCommand = new Command(async () => await ExecuteLoadKnxObjectsCommand());
        }
        /// <summary>
        /// Command for retrieving KNX objects from the database - implementation
        /// </summary>
        /// <returns></returns>
        async Task ExecuteLoadKnxObjectsCommand()
        {
            IsBusy = true;

            try
            {
                KnxObjects.Clear();
                var knxObjects = await DependencyService.Get<IConnector>().KnxObjectsOverview();
                foreach (var knxObject in knxObjects)
                {
                    KnxObjects.Add(knxObject);
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
