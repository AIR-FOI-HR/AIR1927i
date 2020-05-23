using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace KNXcontrol.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", Label = "KNX Control", MainLauncher = true, NoHistory = true)]
    public class Splash : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            intent.AddFlags(ActivityFlags.SingleTop);
            StartActivity(intent);
            Finish();
        }
    }
}