using CoreGraphics;
using Foundation;
using UIKit;

namespace XamagonHunt.iOS
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
            this.Title = "Xamagon Hunt!";

            UIButton shareDemoButton = new UIButton(UIButtonType.System)
            {
                Frame = new CGRect(this.View.Frame.Width / 2 - 40, 150, 75, 44),
                BackgroundColor = UIColor.LightGray.ColorWithAlpha((System.nfloat)0.6)
            };
            shareDemoButton.SetTitle("Start Here", UIControlState.Normal);
            shareDemoButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            this.View.AddSubview(shareDemoButton);

            UILabel shareDemoLabel = new UILabel()
            {
                Text = "List of Anchors? Click Me!",
                TextAlignment = UITextAlignment.Center,
                Frame = new CGRect(10, 200, this.View.Frame.Width - 20, 44),
            };

            UITapGestureRecognizer labelTap = new UITapGestureRecognizer(() => {
                UIApplication.SharedApplication.OpenUrl(new NSUrl("https://github.com/Sweekriti91/TreasureHunt/blob/master/README.md"));
            });

            shareDemoLabel.UserInteractionEnabled = true;
            shareDemoLabel.AddGestureRecognizer(labelTap);

            shareDemoButton.TouchUpInside += (sender, e) =>
            {
                this.NavigationController.PushViewController(new ShareDemoController(), true);
            };
            this.View.AddSubview(shareDemoLabel);
        }
    }
}
