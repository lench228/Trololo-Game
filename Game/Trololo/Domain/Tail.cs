namespace Trololo.Domain
{
    using System.Drawing;

    public class Tail
    {
        public Transform transform; 
        public Image texture;

        public Tail(int x, int y, Image texture1)
        {
            if (texture1 != null)
            {
                transform = new Transform(new Point(x, y), texture1.Size);
                texture = texture1;
            }
            else
            {
                transform = new Transform(new Point(x, y), new Size(0, 0));
                texture = null;
            }
        }
    }
}
