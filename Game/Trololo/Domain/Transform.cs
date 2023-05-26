using System.Drawing;

namespace Trololo.Domain
{
    public class Transform
    {
        public PointF position;
        public RectangleF hitBox;
        public int Direction = 1; 

        public Transform(PointF position, RectangleF box)
        {
            this.position = position;
            hitBox = box;
        }

        public void Move(PointF moove)
        {
            this.position.X += moove.X;
            this.position.Y += moove.Y;
            hitBox.X += moove.X;
            hitBox.Y += moove.Y;
        }
    }
}
