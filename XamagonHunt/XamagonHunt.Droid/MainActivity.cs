﻿using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using XamagonHunt.Common;

namespace XamagonHunt.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/ic_launcher", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private const double MIN_OPENGL_VERSION = 3.0;
        private AnchorSharingServiceClient anchorSharingServiceClient; //cloud saving and retreiving client
        TextView listofAnchors;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Check to see if the device supports SceneForm.
            if (!CheckIsSupportedDeviceOrFinish(this))
            {
                return;
            }
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button basicDemoButton = this.FindViewById<Button>(Resource.Id.arBasicDemo);
            basicDemoButton.Click += this.OnBasicDemoClick;

            TextView links = this.FindViewById<TextView>(Resource.Id.textView3);
            links.Click += OnLinksClick;

            listofAnchors = this.FindViewById<TextView>(Resource.Id.anchorlistitem);
        }

        public async void OnLinksClick(object sender, EventArgs e)
        {
            anchorSharingServiceClient = new AnchorSharingServiceClient(AccountDetails.AnchorSharingServiceUrl);
            var test = await anchorSharingServiceClient.RetrieveAllAnchors();
            var listItemString = string.Empty;
            int count = 0;
            foreach(var item in test)
            {
                count++;
                char[] MyChar = { '[',' ',']' ,'"'};
                string NewString = item.Trim(MyChar);
                listItemString += count.ToString() + ". " + NewString + "\n";
            }
            this.RunOnUiThread(() =>
            {
                listofAnchors.Text = listItemString;
            });
        }

        public void OnBasicDemoClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AzureSpatialAnchorsSharedActivity));
            intent.PutExtra("BasicDemo", true);
            this.StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public static bool CheckIsSupportedDeviceOrFinish(Activity activity)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.N)
            {
                Toast.MakeText(activity, "Sceneform requires Android N or later", ToastLength.Long).Show();

                activity.Finish();

                return false;
            }

            string openglString = ((ActivityManager)activity.GetSystemService(Context.ActivityService)).DeviceConfigurationInfo.GlEsVersion;

            if (double.Parse(openglString) < MIN_OPENGL_VERSION)
            {
                Toast.MakeText(activity, "Sceneform requires OpenGL ES 3.0 or later", ToastLength.Long).Show();

                return false;
            }

            return true;
        }
    }
}

