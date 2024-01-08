using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.IO;

namespace XamarinWordle.Droid
{
    [Activity(Label = "XamarinWordle", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

			//// Copy the words.txt file from assets to the app's data directory
			//string assetsFilePath = "words.txt";
			//string writableFilePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "words.txt");

			//using (var assetStream = Assets.Open(assetsFilePath))
			//using (var writableStream = new FileStream(writableFilePath, FileMode.Create, FileAccess.Write))
			//{
			//	assetStream.CopyTo(writableStream);
			//}

			LoadApplication(new App());
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}