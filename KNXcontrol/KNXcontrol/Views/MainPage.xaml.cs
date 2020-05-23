using System.ComponentModel;
using System.Drawing;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace KNXcontrol.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            SelectedTabColor = Color.AntiqueWhite;
            UnselectedTabColor = Color.White;
            BarBackgroundColor = Color.SteelBlue;
        }
    }
}