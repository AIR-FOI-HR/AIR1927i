using Android.App;
using KNXcontrol.Droid.Classes;
using KNXcontrol.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotifyService))]
namespace KNXcontrol.Droid.Classes
{
    public class NotifyService : IToastService
    {
        public void ShowToast(string message)
        {

        }
    }
}