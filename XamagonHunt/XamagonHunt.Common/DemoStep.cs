using System;
namespace XamagonHunt.Common
{
    public enum DemoStep : uint
    {
        prepare,               // prepare to start
        createCloudAnchor,     // the session will create a cloud anchor
        lookForAnchor,         // the session will look for an anchor
        lookForNearbyAnchors, // the session will look for nearby anchors
        deleteFoundAnchors,    // the session will delete found anchors
        stopSession,            // the session will stop and be cleaned up
        DemoStepChoosing, // Choosing to create or locate
        DemoStepCreating, // Creating an anchor
        DemoStepSaving,   // Saving an anchor to the cloud
        DemoStepEnteringAnchorNumber, // Picking an anchor to find
        DemoStepLocating,  // Looking for an anchor
        DemoScreenshotMode,
        Start,               // prepare to start the demo
        End,                 // the end of the demo
        Restart,             // waiting to restart the demo
        CreateAnchor,        // the session will create a cloud anchor
        SaveAnchor,          // the session will save an anchor
        SavingAnchor,        // the session is in process of saving an anchor
        LocateAnchor,        // the session will look for an anchor
        LocateNearbyAnchors, // the session will look for nearby anchors
        DeleteLocatedANchors,// the session will delete found anchors
        StopSession,         // the session will stop and be cleaned up
        EnterAnchorNumber   // sharing: enter an anchor to find
    }
}
