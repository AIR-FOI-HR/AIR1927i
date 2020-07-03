using KNXcontrol.Services;
using Model.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KNXcontrol.ViewModels
{    /// <summary>
     /// Types viewModel class - loads Types from the database to be displayed and managed in the app
     /// inherits BaseViewModel
     /// </summary>
    class TypesViewModel : BaseViewModel
    {
        public ObservableCollection<Type> Types { get; set; }
        public Command LoadTypesCommand { get; set; }

        public TypesViewModel()
        {
            Types = new ObservableCollection<Type>();
            LoadTypesCommand = new Command(async () => await ExecuteLoadTypesCommand());
        }
        /// <summary>
        /// Command for retrieving types from the database - implementation
        /// </summary>
        /// <returns></returns>
        async Task ExecuteLoadTypesCommand()
        {
            IsBusy = true;

            try
            {
                Types.Clear();
                var types = await DependencyService.Get<IConnector>().TypesOverview();
                foreach (var type in types)
                {
                    Types.Add(type);
                }
            }
            catch
            {
                Types = null;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
