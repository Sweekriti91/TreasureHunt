using ARKit;
using Microsoft.Azure.SpatialAnchors;
using SceneKit;

namespace XamagonDrop.iOS
{
    public class AnchorVisual
    {
        public SCNNode node { get; set; }
        public string identifier { get; set; }
        public CloudSpatialAnchor cloudAnchor { get; set; }
        public ARAnchor localAnchor { get; set; }
    }
}
