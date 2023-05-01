using System.Drawing;

namespace Trololo.Domain
{
    public class Transform
    {
        public PointF position;
        public RectangleF hitBox;

        public Transform(PointF position, RectangleF box)
        {
            this.position = position;
            hitBox = box;
        }
    }
}
