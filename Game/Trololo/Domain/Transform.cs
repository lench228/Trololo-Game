using System.Drawing;

namespace Trololo.Domain
{
    public class Transform
    {
        public Point position;
        public Size size;

        public Transform(Point position, Size size)
        {
            this.position = position;
            this.size = size;
        }
    }

}
