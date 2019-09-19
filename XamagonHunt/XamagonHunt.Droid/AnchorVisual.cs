using System;
using Google.AR.Core;
using Google.AR.Sceneform;
using Google.AR.Sceneform.Math;
using Google.AR.Sceneform.Rendering;
using Google.AR.Sceneform.UX;
using Microsoft.Azure.SpatialAnchors;
using Xamarin.Essentials;

namespace XamagonHunt.Droid
{
    internal class AnchorVisual
    {
        private Material color;
        private Renderable nodeRenderable;

        public AnchorVisual(Anchor localAnchor)
        {
            this.AnchorNode = new AnchorNode(localAnchor);
        }

        public AnchorNode AnchorNode { get; }

        public CloudSpatialAnchor CloudAnchor { get; set; }

        public Anchor LocalAnchor => this.AnchorNode.Anchor;

        public void Render(ArFragment arFragment)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                this.nodeRenderable = ShapeFactory.MakeCube(new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.0f, 0.15f, 0.0f), this.color);
                this.AnchorNode.Renderable = this.nodeRenderable;
                this.AnchorNode.SetParent(arFragment.ArSceneView.Scene);

                TransformableNode sphere = new TransformableNode(arFragment.TransformationSystem);
                sphere.SetParent(this.AnchorNode);
                sphere.Renderable = this.nodeRenderable;
                sphere.Select();
            });
        }

        public void SetColor(Material material)
        {
            if (material is null)
            {
                throw new ArgumentNullException(nameof(material));
            }

            this.color = material;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                this.AnchorNode.Renderable = null;
                this.nodeRenderable = ShapeFactory.MakeSphere(0.1f, new Vector3(0.0f, 0.15f, 0.0f), this.color);
                this.AnchorNode.Renderable = this.nodeRenderable;
            });
        }

        public void Destroy()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                this.AnchorNode.Renderable = null;
                this.AnchorNode.SetParent(null);
            });
        }
    }
}
