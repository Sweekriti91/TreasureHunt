using CoreGraphics;
using Microsoft.Azure.SpatialAnchors;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UIKit;
using XamagonHunt.Common;

namespace XamagonHunt.iOS
{
    public class ShareDemoController : DemoControllerBase
    {
        private readonly string anchorId = string.Empty; //for searching for cloudAnchor
        private readonly AnchorSharingServiceClient anchorSharingServiceClient; //cloud saving and retreiving client

        public string mainLabelText = string.Empty;
        private readonly UILabel mainLabel = new UILabel();
        private readonly UIButton mainButton = new UIButton(UIButtonType.System);
        private readonly UIButton locateButton = new UIButton(UIButtonType.System);
        private readonly UILabel anchorIdLabel = new UILabel();
        private readonly UITextField anchorIdEntry = new UITextField();
        public bool mainButtonisHidden = false;

        public ShareDemoController()
        {
            this.anchorSharingServiceClient = new AnchorSharingServiceClient(AccountDetails.AnchorSharingServiceUrl);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            mainButton.SetTitle("Tap to Locate", UIControlState.Normal);
            mainButton.Frame = new CGRect(10, View.Frame.Height - 90, View.Frame.Width - 20, 44);
            mainButton.BackgroundColor = UIColor.LightGray.ColorWithAlpha((System.nfloat)0.6);
            mainButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            mainButton.Hidden = mainButtonisHidden;
            mainButton.TouchUpInside += MainButton_TouchUpInsideAsync;

            statusLabel.Text = "....";
            statusLabel.TextAlignment = UITextAlignment.Left;
            statusLabel.TextColor = UIColor.White;
            statusLabel.Frame = new CGRect(10, View.Frame.Height - 50, View.Frame.Width - 20, 44);
            statusLabel.Hidden = statusLabelisHidden;

            errorLabel.Text = errorLabelText;
            errorLabel.TextColor = UIColor.White;
            errorLabel.Frame = new CGRect(10, View.Frame.Height - 500, View.Frame.Width - 20, 200);
            errorLabel.LineBreakMode = UILineBreakMode.WordWrap;
            errorLabel.Lines = 10;
            errorLabel.Hidden = errorLabelisHidden;

            anchorIdLabel.Text = "Enter Anchor ID Value:";
            anchorIdLabel.BackgroundColor = UIColor.LightGray;
            anchorIdLabel.TextAlignment = UITextAlignment.Left;
            anchorIdLabel.TextColor = UIColor.White;
            anchorIdLabel.Frame = new CGRect(10, 150, View.Frame.Width - 20, 40);
            anchorIdLabel.Hidden = false;

            anchorIdEntry.Frame = new CGRect(10, 200, View.Frame.Width - 20, 44);
            anchorIdEntry.TextAlignment = UITextAlignment.Left;
            anchorIdEntry.MinimumFontSize = 17f;
            anchorIdEntry.AdjustsFontSizeToFitWidth = true;
            anchorIdEntry.ReturnKeyType = UIReturnKeyType.Done;
            anchorIdEntry.BackgroundColor = UIColor.White;
            anchorIdEntry.KeyboardType = UIKeyboardType.NumberPad;
            anchorIdEntry.Hidden = false;

            this.View.AddSubview(this.mainLabel);
            this.View.AddSubview(this.mainButton);
            this.View.AddSubview(this.locateButton);
            this.View.AddSubview(this.anchorIdLabel);
            this.View.AddSubview(this.anchorIdEntry);
        }

        private async void MainButton_TouchUpInsideAsync(object sender, EventArgs e)
        {
            Debug.WriteLine("CURRENT STEP VALUE :: " + this.step);
            if (this.step == DemoStep.Start)
            {
                this.step = DemoStep.EnterAnchorNumber;
                this.InvokeOnMainThread(() =>
                {
                    this.ignoreMainButtonTaps = true;
                    this.mainButton.Hidden = true;
                    this.locateButton.Hidden = false;
                    this.anchorIdEntry.Hidden = false;
                    this.anchorIdLabel.Hidden = false;
                });
            }
            else
            {
                string inputVal = this.anchorIdEntry.Text;
                this.anchorIdEntry.Hidden = true;
                this.anchorIdLabel.Hidden = true;
                this.locateButton.Hidden = true;
                this.StartSession();
                if (!string.IsNullOrEmpty(inputVal))
                {
                    RetrieveAnchorResponse response = await this.anchorSharingServiceClient.RetrieveAnchorIdAsync(inputVal);

                    Debug.WriteLine("RESPONSE VALUE :: " + response.AnchorId + " ANCHOR Found :: " + response.AnchorFound);

                    if (response.AnchorFound)
                    {
                        this.LookForAnchor(response.AnchorId);
                    }
                    else
                    {
                        this.step = DemoStep.Start;
                        this.anchorIdEntry.Hidden = true;
                        this.anchorIdLabel.Hidden = true;
                        this.UpdateMainStatusTitle("Anchor number not found or has expired.");
                    }

                    this.step = DemoStep.LocateAnchor;
                    this.UpdateMainStatusTitle("Locating Anchor..");
                }
            }
        }

        private async Task LocateButtonTapAsync()
        {
            Debug.WriteLine("CURRENT STEP VALUE :: " + this.step);
            if (this.step == DemoStep.Start)
            {
                this.step = DemoStep.EnterAnchorNumber;
                this.InvokeOnMainThread(() =>
                {
                    this.ignoreMainButtonTaps = true;
                    this.mainButton.Hidden = true;
                    this.locateButton.Hidden = false;
                    this.anchorIdEntry.Hidden = false;
                    this.anchorIdLabel.Hidden = false;
                });
            }
            else
            {
                string inputVal = this.anchorIdEntry.Text;
                this.anchorIdEntry.Hidden = true;
                this.anchorIdLabel.Hidden = true;
                this.locateButton.Hidden = true;
                this.StartSession();
                if (!string.IsNullOrEmpty(inputVal))
                {
                    RetrieveAnchorResponse response = await this.anchorSharingServiceClient.RetrieveAnchorIdAsync(inputVal);

                    Debug.WriteLine("RESPONSE VALUE :: " + response.AnchorId + " ANCHOR Found :: " + response.AnchorFound);

                    if (response.AnchorFound)
                    {
                        this.LookForAnchor(response.AnchorId);
                    }
                    else
                    {
                        this.step = DemoStep.Start;
                        this.anchorIdEntry.Hidden = true;
                        this.anchorIdLabel.Hidden = true;
                        this.UpdateMainStatusTitle("Anchor number not found or has expired.");
                    }

                    this.step = DemoStep.LocateAnchor;
                    this.UpdateMainStatusTitle("Locating Anchor..");
                }
            }
        }

        public override void MoveToNextStepAfterCreateCloudAnchor()
        {
            this.ignoreMainButtonTaps = false;

            this.InvokeOnMainThread(() =>
            {
                this.step = DemoStep.Start;
                this.statusLabel.Text = "Create Success!!";
                this.StopSession();
            });
        }

        public override void MoveToNextStepAfterAnchorLocated()
        {
            this.InvokeOnMainThread(() =>
            {
                this.errorLabel.Hidden = false;
                this.errorLabel.Text = "Anchor Found! Grab a Screenshot!";
                this.statusLabel.Hidden = true;
                this.mainButton.Hidden = true;
            });
        }

        public override void UpdateMainStatusTitle(string title)
        {
            this.InvokeOnMainThread(() => this.mainLabel.Text = title);
        }

        public override void HideMainStatus()
        {
            this.mainLabel.Hidden = true;
        }

        protected override void AnchorSaveSuccess(CloudSpatialAnchor result)
        {
            base.AnchorSaveSuccess(result);

            Debug.WriteLine("ASADemo", "recording anchor with web service");
            Debug.WriteLine("ASADemo", "anchorId: " + this.anchorId);

            Task.Run(async () =>
            {
                try
                {
                    SendAnchorResponse sendResult = await this.SendtoSharingServiceAsync(this.cloudAnchor.Identifier);
                    this.UpdateMainStatusTitle("Anchor Number: " + sendResult.AnchorNumber);
                    this.MoveToNextStepAfterCreateCloudAnchor();
                }
                catch (Exception ex)
                {
                    this.AnchorSaveFailed(ex.Message);
                }
            });
        }

        public async Task<SendAnchorResponse> SendtoSharingServiceAsync(string anchorId)
        {
            SendAnchorResponse response = null;

            if (anchorId == null)
            {
                throw new ArgumentException("The anchorId was null");
            }

            try
            {
                response = await this.anchorSharingServiceClient.SendAnchorIdAsync(anchorId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return response;
        }
    }
}
