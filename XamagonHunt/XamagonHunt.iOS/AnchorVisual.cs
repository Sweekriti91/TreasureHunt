using ARKit;
using Microsoft.Azure.SpatialAnchors;
using SceneKit;

namespace XamagonHunt.iOS
{
    public class AnchorVisual
    {
        public SCNNode node { get; set; }
        public string identifier { get; set; }
        public CloudSpatialAnchor cloudAnchor { get; set; }
        public ARAnchor localAnchor { get; set; }
    }
}
