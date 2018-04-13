using System;
using System.Diagnostics;
using System.Text;
using UIKit;
using Virgil.Crypto;

namespace Tests.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            VirgilVersion.AsString();
            System.Console.WriteLine("VirgilVersion=" + VirgilVersion.AsString());
            // if you want to use a different Application Delegate class from "UnitTestAppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "UnitTestAppDelegate");
        }
    }
}
