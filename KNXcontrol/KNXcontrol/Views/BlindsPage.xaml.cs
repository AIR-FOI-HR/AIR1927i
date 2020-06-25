using KNXcontrol.Configuration;
using KNXcontrol.Models;
using KNXcontrol.ServicesImplementation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KNXcontrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlindsPage : ContentPage
    {
        private readonly RoomsService RoomsService = new RoomsService();
        private readonly KnxObjectsService KnxObjectsService = new KnxObjectsService();
        private readonly BlindsService BlindsService = new BlindsService();
        public BlindsPage()
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
                        if (knxObject.Type._id.ToString() == TypeIds.JaulousineMove)
                        {
                            var horizontalStack = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal
                            };

                            var down = new Button
                            {
                                Text = "Dolje",
                            };
                            down.Clicked += delegate (object sender, EventArgs e) { MoveBlindsDown(sender, e, knxObject); };

                            var up = new Button
                            {
                                Text = "Gore",
                            };
                            up.Clicked += delegate (object sender, EventArgs e) { MoveBlindsUp(sender, e, knxObject); };

                            var title = new Label
                            {
                                FontSize = 12,
                                Margin = new Thickness(10, 5, 10, 0),
                                Text = knxObject.Description
                            };
                            horizontalStack.Children.Add(down);
                            horizontalStack.Children.Add(up);

                            var completeStack = new StackLayout();
                            completeStack.Children.Add(title);
                            completeStack.Children.Add(horizontalStack);

                            var frame = new Frame
                            {
                                Margin = new Thickness(10, 5, 10, 5)
                            };
                            frame.Content = completeStack;

                            stkLayout.Children.Add(frame);
                        }
                        else if (knxObject.Type._id.ToString() == TypeIds.JaulousineRotate)
                        {
                            var horizontalStack = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal
                            };

                            var rotateDown = new Button
                            {
                                Text = "Rotiraj dolje",
                            };
                            rotateDown.Clicked += delegate (object sender, EventArgs e) { RotateDown(sender, e, knxObject); };
                            var valLabel = new Label
                            {
                                BindingContext = knxObject,
                                Text = knxObject.Value,
                                VerticalTextAlignment = TextAlignment.Center
                            };
                            valLabel.SetBinding(Label.TextProperty, knxObject.Value);
                            var rotateUp = new Button
                            {
                                Text = "Rotiraj gore",
                            };
                            rotateUp.Clicked += delegate (object sender, EventArgs e) { RotateUp(sender, e, knxObject); };
                            var title = new Label
                            {
                                FontSize = 12,
                                Margin = new Thickness(10, 5, 10, 0),
                                Text = knxObject.Description
                            };
                            horizontalStack.Children.Add(rotateDown);
                            horizontalStack.Children.Add(valLabel);
                            horizontalStack.Children.Add(rotateUp);

                            var titleStack = new StackLayout();
                            titleStack.Children.Add(title);
                            var completeStack = new StackLayout();
                            completeStack.Children.Add(titleStack);
                            completeStack.Children.Add(horizontalStack);

                            var frame = new Frame
                            {
                                Margin = new Thickness(10, 5, 10, 5)
                            };
                            frame.Content = completeStack;
                            stkLayout.Children.Add(frame);
                        }
                    }
                }
            }
        }

        private void MoveBlindsDown(object sender, EventArgs e, KnxObject knxObj)
        {
            try
            {
                knxObj.Value = "0";
                _ = BlindsService.Move(knxObj);
                _ = KnxObjectsService.UpdateKnxObject(knxObj);
            }
            catch
            {

            }
        }

        private void MoveBlindsUp(object sender, EventArgs e, KnxObject knxObj)
        {
            try
            {
                knxObj.Value = "1";
                _ = BlindsService.Move(knxObj);
                _ = KnxObjectsService.UpdateKnxObject(knxObj);
            }
            catch
            {

            }
        }

        private void RotateUp(object sender, EventArgs e, KnxObject knxObj)
        {
            try
            {
                if(int.Parse(knxObj.Value) < 7)
                {
                    knxObj.Value = (int.Parse(knxObj.Value) + 1).ToString();
                    _ = BlindsService.Rotate(knxObj);
                    _ = KnxObjectsService.UpdateKnxObject(knxObj);
                }
            }
            catch
            {

            }
        }

        private void RotateDown(object sender, EventArgs e, KnxObject knxObj)
        {
            try
            {
                if (int.Parse(knxObj.Value) > 0)
                {
                    knxObj.Value = (int.Parse(knxObj.Value) - 1).ToString();
                    _ = BlindsService.Rotate(knxObj);
                    _ = KnxObjectsService.UpdateKnxObject(knxObj);
                }
            }
            catch
            {

            }
        }
    }
}