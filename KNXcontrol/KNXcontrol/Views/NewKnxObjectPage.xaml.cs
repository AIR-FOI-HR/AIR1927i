using KNXcontrol.Models;
using KNXcontrol.Services;
using KNXcontrol.ServicesImplementation;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KNXcontrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewKnxObjectPage : ContentPage
    {
        public KnxObject KnxObject { get; set; }
        private readonly KnxObjectsService KnxObjectsService = new KnxObjectsService();
        public List<string> DataPointTypes { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Models.Type> Types { get; set; }
        public bool IsUpdate { get; set; }
        /// <summary>
        /// Constructor for managing knx objects - gets object if update, else null and list of rooms/types to be selected
        /// </summary>
        /// <param name="knxObject"></param>
        /// <param name="rooms"></param>
        /// <param name="types"></param>
        public NewKnxObjectPage(KnxObject knxObject, List<Room> rooms, List<Models.Type> types)
        {
            InitializeComponent();

            KnxObject = knxObject ?? new KnxObject();
            IsUpdate = knxObject != null ? true : false;

            Title = IsUpdate ? "Uredi KNX objekt" : "Novi KNX objekt";

            Rooms = rooms;
            Types = types;

            var dataPointTypes = new DPT();
            DataPointTypes = dataPointTypes.DPTs;

            if (IsUpdate)
            {
                for (int i = 0; i < types.Count; i++)
                {
                    if (types[i]._id == knxObject.Type._id)
                    {
                        tip.SelectedIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (rooms[i]._id == knxObject.Room._id)
                    {
                        prostorija.SelectedIndex = i;
                        break;
                    }
                }
            }

            BindingContext = this;
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            Cancel.Clicked += null;
            Cancel.IsEnabled = false;
            await Navigation.PopModalAsync();
        }
        /// <summary>
        /// Validates user input via Regex/other checks and based on the IsUpdate flag, updates or creates a new object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Save_Clicked(object sender, EventArgs e)
        {
            var regexPattern = "[0-9]{1,3}/{1}[0-9]{1,3}/{1}[0-9]{1,3}";
            if (string.IsNullOrEmpty(KnxObject.Address))
                DependencyService.Get<IToastService>().ShowToast("Adresa je obavezna!");
            else if (!Regex.Match(KnxObject.Address, regexPattern).Success)
                DependencyService.Get<IToastService>().ShowToast("Adresa nije u dobrom formatu, primjer: 123/321/456!");
            else if (string.IsNullOrEmpty(KnxObject.Value))
                DependencyService.Get<IToastService>().ShowToast("Vrijednost je obavezna!");
            else if (string.IsNullOrEmpty(KnxObject.DPT))
                DependencyService.Get<IToastService>().ShowToast("DPT je obavezan!");
            else if (KnxObject.Room == null || KnxObject.Room._id.Equals(Guid.Empty))
                DependencyService.Get<IToastService>().ShowToast("Prostorija je obavezna!");
            else if (KnxObject.Type == null || KnxObject.Type._id.Equals(Guid.Empty))
                DependencyService.Get<IToastService>().ShowToast("Tip je obavezan!");
            else
            {
                Save.Clicked += null;
                Save.IsEnabled = false;
                if (IsUpdate)
                {
                    await KnxObjectsService.UpdateKnxObject(KnxObject);
                    DependencyService.Get<IToastService>().ShowToast("KNX objekt je uspješno ažuriran!");
                    MessagingCenter.Send(this, "UPDATE", KnxObject);
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await KnxObjectsService.AddKnxObject(KnxObject);
                    DependencyService.Get<IToastService>().ShowToast("KNX objekt je uspješno kreiran!");
                    MessagingCenter.Send(this, "CREATE", KnxObject);
                    await Navigation.PopModalAsync();
                }
            }
        }
        /// <summary>
        /// On type change, sets the types default value to the value field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTypePickChange(object sender, EventArgs e)
        {
            if (KnxObject.Type != null)
            {
                KnxObject.Value = KnxObject.Type.DefaultValue;
                vrijednost.Text = KnxObject.Value;
                DependencyService.Get<IToastService>().ShowToast("Vrijednost je automatski postavljena prema tipu!");
            }
        }
    }
}