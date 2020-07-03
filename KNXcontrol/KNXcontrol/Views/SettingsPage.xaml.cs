using KNXcontrol.Services;
using Model.Configuration;
using Model.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KNXcontrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
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
        /// <summary>
        /// Method for central control of lighting - gets central function address from config and switches all the lights on/off
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Lights_Clicked(object sender, EventArgs e)
        {
            _ = DependencyService.Get<IConnector>().Switch(new KnxObject
            {
                Address = Config.LightsCentral,
                Value = LightValue == 0 ? 1.ToString() : 0.ToString(),
                DPT = "DPT1"
            });
            LightValue = LightValue == 0 ? 1 : 0;

            var allObjects = await DependencyService.Get<IConnector>().KnxObjectsOverview();
            foreach (KnxObject knxObject in allObjects)
            {
                if(knxObject.Type._id.ToString() == TypeIds.LightRegular)
                {
                    knxObject.Value = LightValue.ToString();
                    _ = DependencyService.Get<IConnector>().UpdateKnxObject(knxObject);
                }else if(knxObject.Type._id.ToString() == TypeIds.LightDimmable)
                {
                    var val = LightValue == 0 ? 0 : 255;
                    knxObject.Value = val.ToString();
                    _ = DependencyService.Get<IConnector>().UpdateKnxObject(knxObject);
                }
            }
        }
        /// <summary>
        /// Method for central control of blinds - gets central function address from config and moves all blinds up/down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Blinds_Clicked(object sender, EventArgs e)
        {
            _ = DependencyService.Get<IConnector>().Move(new KnxObject
            {
                Address = Config.JaulousineCentral,
                Value = BlindsValue == 0 ? 1.ToString() : 0.ToString(),
                DPT = "DPT1"
            });
            BlindsValue = BlindsValue == 0 ? 1 : 0;
        }
    }
}