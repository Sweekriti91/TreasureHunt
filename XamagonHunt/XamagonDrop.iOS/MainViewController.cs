using CoreGraphics;
using UIKit;

namespace XamagonDrop.iOS
{
    public class MainViewController : UIViewController
    {
        public MainViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.White;
            this.Title = "Xamagon Drop";

            UIButton shareDemoButton = new UIButton(UIButtonType.System)
            {
                Frame = new CGRect(this.View.Frame.Width / 2 - 80, 150, 150, 44),
                BackgroundColor = UIColor.LightGray.ColorWithAlpha((System.nfloat)0.6)
            };
            shareDemoButton.SetTitle("Drop Anchors, ARrrr", UIControlState.Normal);
            shareDemoButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            shareDemoButton.TouchUpInside += (sender, e) =>
            {
                this.NavigationController.PushViewController(new ShareDemoController(), true);
            };

            UILabel shareDemoLabel = new UILabel()
            {
                Text = "Anchors Away!",
                TextAlignment = UITextAlignment.Center,
                Frame = new CGRect(10, 200, this.View.Frame.Width - 20, 44),
            };

            this.View.AddSubview(shareDemoButton);
            this.View.AddSubview(shareDemoLabel);
        }
    }
}
