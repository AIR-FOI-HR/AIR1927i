using KNXcontrol.Services;
using Model.Configuration;
using Model.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KNXcontrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlindsPage : ContentPage
    {
        public BlindsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Setup();
        }
        /// <summary>
        /// Setup method that loads KNX object for each room, then sets up the controls based on type 
        /// Adds controls for moving the blinds up/down and rotattion
        /// Gets called in the OnAppearing method - everytime the current page is opened
        /// </summary>
        private async void Setup()
        {
            stkLayout.Children.Clear();
            var rooms = await DependencyService.Get<IConnector>().RoomsOverview();

            foreach (var room in rooms)
            {
                var knxObjectsInRoom = await DependencyService.Get<IConnector>().KnxObjectsByRoom(room._id.ToString());
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
        /// <summary>
        /// Calls service to move the blinds down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="knxObj"></param>
        private void MoveBlindsDown(object sender, EventArgs e, KnxObject knxObj)
        {
            try
            {
                knxObj.Value = "0";
                _ = DependencyService.Get<IConnector>().Move(knxObj);
                _ = DependencyService.Get<IConnector>().UpdateKnxObject(knxObj);
            }
            catch
            {

            }
        }
        /// <summary>
        /// Calls service to move the blinds up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="knxObj"></param>
        private void MoveBlindsUp(object sender, EventArgs e, KnxObject knxObj)
        {
            try
            {
                knxObj.Value = "1";
                _ = DependencyService.Get<IConnector>().Move(knxObj);
                _ = DependencyService.Get<IConnector>().UpdateKnxObject(knxObj);
            }
            catch
            {

            }
        }
        /// <summary>
        /// Calls service to rorate the blinds up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="knxObj"></param>
        private void RotateUp(object sender, EventArgs e, KnxObject knxObj)
        {
            try
            {
                if(int.Parse(knxObj.Value) < 7)
                {
                    knxObj.Value = (int.Parse(knxObj.Value) + 1).ToString();
                    _ = DependencyService.Get<IConnector>().Rotate(knxObj);
                    _ = DependencyService.Get<IConnector>().UpdateKnxObject(knxObj);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// Calls service to rorate the blinds up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="knxObj"></param>
        private void RotateDown(object sender, EventArgs e, KnxObject knxObj)
        {
            try
            {
                if (int.Parse(knxObj.Value) > 0)
                {
                    knxObj.Value = (int.Parse(knxObj.Value) - 1).ToString();
                    _ = DependencyService.Get<IConnector>().Rotate(knxObj);
                    _ = DependencyService.Get<IConnector>().UpdateKnxObject(knxObj);
                }
            }
            catch
            {

            }
        }
    }
}