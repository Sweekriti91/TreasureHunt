﻿using CoreGraphics;
using Microsoft.Azure.SpatialAnchors;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UIKit;
using XamagonHunt.Common;

namespace XamagonDrop.iOS
{
    public class ShareDemoController : DemoControllerBase
    {
        private readonly string anchorId = string.Empty; //for searching for cloudAnchor
        private readonly AnchorSharingServiceClient anchorSharingServiceClient; //cloud saving and retreiving client

        public string mainLabelText = string.Empty;
        private readonly UILabel mainLabel = new UILabel();
        private readonly UIButton createButton = new UIButton(UIButtonType.System);

        public ShareDemoController()
        {
            this.anchorSharingServiceClient = new AnchorSharingServiceClient(AccountDetails.AnchorSharingServiceUrl);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            step = DemoStep.Start;

            this.createButton.SetTitle("Create", UIControlState.Normal);
            this.createButton.Frame = new CGRect(10, View.Frame.Height - 90, View.Frame.Width - 20, 44);
            this.createButton.BackgroundColor = UIColor.LightGray.ColorWithAlpha((nfloat)0.6);
            this.createButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            this.createButton.TouchUpInside += (sender, e) => this.CreateButtonTap();

            this.statusLabel.Text = "....";
            this.statusLabel.TextAlignment = UITextAlignment.Left;
            this.statusLabel.TextColor = UIColor.White;
            this.statusLabel.Frame = new CGRect(10, this.View.Frame.Height - 50, this.View.Frame.Width - 20, 44);
            this.statusLabel.Hidden = this.statusLabelisHidden;

            this.mainLabel.Text = "";
            this.mainLabel.TextAlignment = UITextAlignment.Center;
            this.mainLabel.LineBreakMode = UILineBreakMode.WordWrap;
            this.mainLabel.Lines = 3;
            this.mainLabel.TextColor = UIColor.Yellow;
            this.mainLabel.Frame = new CGRect(10, 150, this.View.Frame.Width - 20, 40);


            this.View.AddSubview(this.mainLabel);
            this.View.AddSubview(this.createButton);
        }

        private void CreateButtonTap()
        {
            this.step = DemoStep.CreateAnchor;
            this.ignoreMainButtonTaps = true;
            this.currentlyPlacingAnchor = true;
            this.saveCount = 0;
            this.createButton.Hidden = true;
            this.StartSession();

            this.UpdateMainStatusTitle("Tap on the screen to Create an Anchor");
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
                this.mainLabel.Text = "Anchor Found!";
                this.statusLabel.Hidden = true;
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
