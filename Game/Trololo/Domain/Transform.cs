using System.Drawing;
using System.Xml.Serialization;

namespace Trololo.Domain
{
    public class Transform
    {
        private PointF position;
        public PointF Position
        {
            get { return position; }
            set { position = value; }
        }

        private RectangleF hitBox;
        public RectangleF HitBox
        {
            get { return hitBox; }
            set { hitBox = value; }
        }

        private int direction;
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Transform(PointF position, RectangleF box)
        {
            Position = position;
            HitBox = box;
        }

        public void Move(PointF move)
        {
            Position = new PointF(Position.X + move.X, Position.Y + move.Y);
            HitBox = new RectangleF(HitBox.X + move.X, HitBox.Y + move.Y, HitBox.Width, HitBox.Height);
        }
    }
}
