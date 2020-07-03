using KNXcontrol.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Type = Model.Models.Type;
using EventArgs = System.EventArgs;

namespace KNXcontrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTypePage : ContentPage
    {
        public Type Type { get; set; }
        public bool IsUpdate { get; set; }
        /// <summary>
        /// Constructor for managing types - gets types if update, else null
        /// </summary>
        /// <param name="knxObject"></param>
        /// <param name="rooms"></param>
        /// <param name="types"></param>
        public NewTypePage(Type type)
        {
            InitializeComponent();

            Type = type ?? new Type();
            IsUpdate = type != null ? true : false;

            Title = IsUpdate ? "Uredi tip" : "Novi tip";

            BindingContext = this;
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            Cancel.Clicked += null;
            Cancel.IsEnabled = false;
            await Navigation.PopModalAsync();
        }
        /// <summary>
        /// Validates user input and based on the IsUpdate flag, updates or creates a new type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    await DependencyService.Get<IConnector>().UpdateType(Type);
                    DependencyService.Get<IToastService>().ShowToast("Tip je uspješno ažuriran!");
                    MessagingCenter.Send(this, "UPDATE", Type);
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DependencyService.Get<IConnector>().AddType(Type);
                    DependencyService.Get<IToastService>().ShowToast("Tip je uspješno kreiran!");
                    MessagingCenter.Send(this, "CREATE", Type);
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}