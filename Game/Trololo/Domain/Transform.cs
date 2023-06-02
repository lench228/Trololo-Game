using System.Drawing;
using System.Xml.Serialization;

namespace Trololo.Domain
{
    public class Transform
    {
        private PointF _position;
        public PointF Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private RectangleF _hitBox;
        public RectangleF HitBox
        {
            get { return _hitBox; }
            set { _hitBox = value; }
        }

        private int _direction;
        public int Direction
        {
            get { return _direction; }
            set { _direction = value; }
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
