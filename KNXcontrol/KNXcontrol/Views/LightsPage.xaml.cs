using Flurl.Http;
using KNXcontrol.Configuration;
using KNXcontrol.Models;
using KNXcontrol.ServicesImplementation;
using System;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Forms;

namespace KNXcontrol.Views
{
    [DesignTimeVisible(false)]
    public partial class LightsPage : ContentPage
    {
        private readonly RoomsService RoomsService = new RoomsService();
        private readonly KnxObjectsService KnxObjectsService = new KnxObjectsService();
        private readonly LightsService LightsService = new LightsService();
        public LightsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Setup();
        }

        private async void Setup()
        {
            stkLayout.Children.Clear();
            var rooms = await RoomsService.RoomsOverview();

            foreach (var room in rooms)
            {
                var knxObjectsInRoom = await KnxObjectsService.KnxObjectsByRoom(room._id.ToString());
                if (knxObjectsInRoom.Count > 0)
                {
                    var headerLabel = new Label
                    {
                        Text = room.Name,
                        FontSize = 20,
                        Margin = new Thickness(15, 5, 0, 0),
                        FontAttributes = FontAttributes.Bold
                    };
                    stkLayout.Children.Add(headerLabel);
                    foreach (var knxObject in knxObjectsInRoom)
                    {
                        if (knxObject.Type._id.ToString() == TypeIds.LightDimmable)
                        {
                            var slider = new Slider
                            {
                                Margin = new Thickness(20, 10, 20, 0),
                                Maximum = 255,
                                Minimum = 0,
                                Value = double.Parse(knxObject.Value),
                                MinimumTrackColor = Color.Yellow,
                                MaximumTrackColor = Color.Black,
                                ScaleY = 2
                            };
                            slider.DragCompleted += delegate (object sender, EventArgs e) { Slider_ValueChanged(sender, e, knxObject); };
                            var description = new Label
                            {
                                FontSize = 12,
                                Margin = new Thickness(20, 5, 20, 0),
                                Text = knxObject.Description
                            };

                            stkLayout.Children.Add(description);
                            stkLayout.Children.Add(slider);
                        }
                        else if (knxObject.Type._id.ToString() == TypeIds.LightRegular)
                        {
                            var toggle = new Switch
                            {
                                IsToggled = knxObject.Value == "0" ? false : true,
                                HorizontalOptions = LayoutOptions.EndAndExpand,
                                Scale = 1.3,
                                Margin = new Thickness(0, 10, 20, 0)
                            };

                            toggle.Toggled += delegate (object sender, ToggledEventArgs e) { ToggleChange(sender, e, knxObject); };
                            var description = new Label
                            {
                                FontSize = 12,
                                Margin = new Thickness(20, 5, 20, 0),
                                Text = knxObject.Description
                            };
                            var layout = new StackLayout()
                            {
                                Orientation = StackOrientation.Horizontal
                            };

                            layout.Children.Add(description);
                            layout.Children.Add(toggle);
                            stkLayout.Children.Add(layout);
                        }
                    }
                }
            }
        }

        private void Slider_ValueChanged(object sender, EventArgs e, KnxObject knxObj)
        {
            try
            {
                Slider slid = sender as Slider;
                knxObj.Value = ((int)slid.Value).ToString();
                LightsService.Dim(knxObj);
                KnxObjectsService.UpdateKnxObject(knxObj);
            }
            catch
            {

            }
        }

        private void ToggleChange(object sender, ToggledEventArgs e, KnxObject knxObj)
        {
            try
            {
                Switch toggle = sender as Switch;
                knxObj.Value = toggle.IsToggled ? "1" : "0";
                LightsService.Switch(knxObj);
                KnxObjectsService.UpdateKnxObject(knxObj);
            }
            catch
            {

            }
        }
    }
}