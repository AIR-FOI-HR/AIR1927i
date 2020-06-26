using Android.Widget;
using KNXcontrol.Droid.Classes;
using KNXcontrol.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastService))]
namespace KNXcontrol.Droid.Classes
{
    public class ToastService : IToastService
    {
        public ToastService()
        {

        }
        /// <summary>
        /// Implements IToastService - shows notification(message) to the user
        /// </summary>
        /// <param name="message"></param>
        public void ShowToast(string message)
        {
            Toast.MakeText(Forms.Context, message, ToastLength.Short).Show();
        }
    }
}