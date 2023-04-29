using System.Drawing;

namespace Trololo.Domain
{
    public class Transform
    {
        public PointF position;
        public Size size;
        public RectangleF rect;

        public Transform(PointF position, Size size)
        {
            this.position = position;
            this.size = size;
            this.rect = new RectangleF(position.X, position.Y, 140, 140);
        }
    }
}
