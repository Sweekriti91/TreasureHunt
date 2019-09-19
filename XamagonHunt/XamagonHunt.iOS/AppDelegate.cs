using Foundation;
using UIKit;

namespace XamagonHunt.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create a new window instance based on the screen size
            this.Window = new UIWindow(UIScreen.MainScreen.Bounds);

            MainViewController mainViewController = new MainViewController();

            //set root to navigation controller
            this.Window.RootViewController = new UINavigationController(mainViewController);

            // make the window visible
            this.Window.MakeKeyAndVisible();
            return true;
        }
    }
}

