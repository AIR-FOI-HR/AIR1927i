﻿using KNXcontrol.Models;
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
        public NewKnxObjectPage(KnxObject knxObject, List<Room> rooms, List<Models.Type> types)
        {
            InitializeComponent();

            KnxObject = knxObject ?? new KnxObject();
            IsUpdate = knxObject != null ? true : false;

            Rooms = rooms;
            Types = types;
            var dataPointTypes = new DPT();
            DataPointTypes = dataPointTypes.DPTs;

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

        private void OnTypePickChange(object sender, EventArgs e)
        {
            if (KnxObject.Type != null)
            {
                KnxObject.Value = KnxObject.Type.DefaultValue;
                Vrijednost.Text = KnxObject.Value;
                DependencyService.Get<IToastService>().ShowToast("Vrijednost je automatski postavljena prema tipu!");
            }
        }
    }
}