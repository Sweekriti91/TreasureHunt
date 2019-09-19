using OpenTK;
using SceneKit;

namespace XamagonHunt.iOS
{
    public static class NMatrix4Extensions
    {
        public static SCNVector3 ToPosition(this NMatrix4 transform)
        {
            return new SCNVector3(transform.M14, transform.M24, transform.M34);
        }
    }
}
