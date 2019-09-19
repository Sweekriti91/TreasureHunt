using CoreGraphics;
using Foundation;
using UIKit;
using XamagonHunt.Common;

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
            var anchorSharingServiceClient = new AnchorSharingServiceClient(AccountDetails.AnchorSharingServiceUrl);

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

            UILabel listOfAnchors = new UILabel()
            {
                Text = "...",
                TextAlignment = UITextAlignment.Natural,
                Frame = new CGRect(10, 230, this.View.Frame.Width - 20, 300),
                //LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 10
            };

            UITapGestureRecognizer labelTap = new UITapGestureRecognizer(async () => {
                var test = await anchorSharingServiceClient.RetrieveAllAnchors();
                var listItemString = string.Empty;
                int count = 0;
                foreach (var item in test)
                {
                    count++;
                    char[] MyChar = { '[', ' ', ']', '"' };
                    string NewString = item.Trim(MyChar);
                    listItemString += count.ToString() + ". " + NewString + "\n";
                }

                listOfAnchors.Text = listItemString;
            });

            shareDemoLabel.UserInteractionEnabled = true;
            shareDemoLabel.AddGestureRecognizer(labelTap);

            shareDemoButton.TouchUpInside += (sender, e) =>
            {
                this.NavigationController.PushViewController(new ShareDemoController(), true);
            };
            this.View.AddSubview(shareDemoLabel);
            this.View.AddSubview(listOfAnchors);
        }
    }
}
