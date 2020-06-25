using KNXcontrol.Configuration;
using KNXcontrol.Models;
using KNXcontrol.ServicesImplementation;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KNXcontrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public LightsService LightsService = new LightsService();
        public BlindsService BlindsService = new BlindsService();
        public KnxObjectsService KnxObjectsService = new KnxObjectsService();
        private int LightValue = 0;
        private int BlindsValue = 0;
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void RoomsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new RoomsOverviewPage()));
        }

        private async void KnxObjectsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new KnxObjectsOverviewPage()));
        }

        private async void TypesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new TypesOverviewPage()));
        }

        private async void Lights_Clicked(object sender, EventArgs e)
        {
            _ = LightsService.Switch(new KnxObject
            {
                Address = Config.LightsCentral,
                Value = LightValue == 0 ? 1.ToString() : 0.ToString(),
                DPT = "DPT1"
            });
            LightValue = LightValue == 0 ? 1 : 0;

            var allObjects = await KnxObjectsService.KnxObjectsOverview();
            foreach (KnxObject knxObject in allObjects)
            {
                if(knxObject.Type._id.ToString() == TypeIds.LightRegular)
                {
                    knxObject.Value = LightValue.ToString();
                    _ = KnxObjectsService.UpdateKnxObject(knxObject);
                }else if(knxObject.Type._id.ToString() == TypeIds.LightDimmable)
                {
                    var val = LightValue == 0 ? 0 : 255;
                    knxObject.Value = val.ToString();
                    _ = KnxObjectsService.UpdateKnxObject(knxObject);
                }
            }
        }

        private void Blinds_Clicked(object sender, EventArgs e)
        {
            _ = BlindsService.Move(new KnxObject
            {
                Address = Config.JaulousineCentral,
                Value = BlindsValue == 0 ? 1.ToString() : 0.ToString(),
                DPT = "DPT1"
            });
            BlindsValue = BlindsValue == 0 ? 1 : 0;
        }
    }
}