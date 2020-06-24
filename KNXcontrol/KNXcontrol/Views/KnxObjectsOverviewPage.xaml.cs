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
    public partial class KnxObjectsOverviewPage : ContentPage
    {
        readonly KnxObjectViewModel viewModel;
        private readonly KnxObjectsService knxObjectsService = new KnxObjectsService();
        private readonly RoomsService RoomsService = new RoomsService();
        private readonly TypesService TypesService = new TypesService();

        public KnxObjectsOverviewPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new KnxObjectViewModel();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.KnxObjects?.Count == 0)
                viewModel.IsBusy = true;
        }

        private async void AddKnxObject_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewKnxObjectPage(null, await RoomsService.RoomsOverview(), await TypesService.TypesOverview())));
            MessagingCenter.Subscribe<NewKnxObjectPage, KnxObject>(this, "CREATE", (page, knxObject) =>
            {
                viewModel.KnxObjects.Add(knxObject);
            });
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var id = ((Button)sender).CommandParameter.ToString();

            bool answer = await DisplayAlert("Brisanje prostorije", "Jeste li sigurni da želite obrisati KNX objekt?", "Da", "Odustani");

            if (answer)
            {
                var result = await knxObjectsService.DeleteKnxObject(id);
                if (result)
                {
                    DependencyService.Get<IToastService>().ShowToast("KNX objekt je uspješno obrisan!");
                    var deletedItem = viewModel.KnxObjects.ToList().Find(x => x._id.ToString() == id);
                    viewModel.KnxObjects.Remove(deletedItem);
                }
            }
        }

        private async void OnKnxObjectSelection(object sender, EventArgs e)
        {
            var knxObject = ((TappedEventArgs)e).Parameter as KnxObject;

            await Navigation.PushModalAsync(new NavigationPage(new NewKnxObjectPage(knxObject, await RoomsService.RoomsOverview(), await TypesService.TypesOverview())));
            MessagingCenter.Subscribe<NewKnxObjectPage, KnxObject>(this, "UPDATE", (page, newKnxObject) =>
            {
                var oldKnxObject = viewModel.KnxObjects.FirstOrDefault(x => x._id == knxObject._id);
                int i = viewModel.KnxObjects.IndexOf(oldKnxObject);
                viewModel.KnxObjects[i] = newKnxObject;
            });
        }
    }
}