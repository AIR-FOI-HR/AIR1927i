using KNXcontrol.Models;
using KNXcontrol.ServicesImplementation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KNXcontrol.ViewModels
{
    class TypesViewModel : BaseViewModel
    {
        public ObservableCollection<Type> Types { get; set; }
        public Command LoadTypesCommand { get; set; }
        private readonly TypesService typesService = new TypesService();

        public TypesViewModel()
        {
            Types = new ObservableCollection<Type>();
            LoadTypesCommand = new Command(async () => await ExecuteLoadTypesCommand());
        }

        async Task ExecuteLoadTypesCommand()
        {
            IsBusy = true;

            try
            {
                Types.Clear();
                var types = await typesService.TypesOverview();
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
