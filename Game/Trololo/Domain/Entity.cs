using System.Drawing; 

namespace Trololo.Domain
{
    public class Entity
    {
        public Transform transform = new Transform(new PointF(0,0), new RectangleF()) ;
        public static int health;
        public static float velocity;
        public static float gravity = (float)1.25;
        public  Image texture;


        public void Move(PointF moove)
        {
            transform.position.X += moove.X;
            transform.position.Y += moove.Y;
        }

        public void SetTransform(PointF position)
        {
            transform.position = position;
            transform.hitBox = new RectangleF(position.X, position.Y, texture.Width, texture.Height); 
        }
    }
}
