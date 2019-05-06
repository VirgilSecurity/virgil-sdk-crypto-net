using Android.App;
using Android.Widget;
using Android.OS;
using Virgil.Crypto;

namespace Crypto.Example.Droid
{
    [Activity(Label = "Crypto.Example.Droid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            System.Console.WriteLine("VirgilVersion=" + VirgilVersion.AsString());
            var s = new Virgil.Crypto.Pfs.VirgilPFS();
            var v = new Virgil.Crypto.VirgilCrypto();
            var p = new Virgil.Crypto.Pythia.VirgilPythia();

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { button.Text = $"{count++} clicks!"; };
        }
    }
}

