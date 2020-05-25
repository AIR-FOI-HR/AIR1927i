using KNXcontrol.Models;
using KNXcontrol.Services;
using KNXcontrol.ServicesImplementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EventArgs = System.EventArgs;

namespace KNXcontrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTypePage : ContentPage
    {
        public Type Type { get; set; }
        private readonly TypesService typesService = new TypesService();
        public bool IsUpdate { get; set; }
        public NewTypePage(Type type)
        {
            InitializeComponent();

            Type = type ?? new Type();
            IsUpdate = type != null ? true : false;

            BindingContext = this;
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            Cancel.Clicked += null;
            Cancel.IsEnabled = false;
            await Navigation.PopModalAsync();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Type.Name))
                DependencyService.Get<IToastService>().ShowToast("Naziv je obavezan!");
            else if (string.IsNullOrEmpty(Type.DefaultValue))
                DependencyService.Get<IToastService>().ShowToast("Zadana vrijednost je obavezna!");
            else
            {
                Save.Clicked += null;
                Save.IsEnabled = false;
                if (IsUpdate)
                {
                    await typesService.UpdateType(Type);
                    DependencyService.Get<IToastService>().ShowToast("Tip je uspješno ažuriran!");
                    MessagingCenter.Send(this, "UPDATE", Type);
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await typesService.AddType(Type);
                    DependencyService.Get<IToastService>().ShowToast("Tip je uspješno kreiran!");
                    MessagingCenter.Send(this, "CREATE", Type);
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}