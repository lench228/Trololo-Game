using System.Drawing;
using Trololo.View;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Data;

namespace Trololo.Domain
{
    public class Player
    {
        public static Transform transform;
        public static Image texture;
        public static int health;
        public static float velocity;
        public static float gravity = (float)9.8; 

        static Player()
        {
            transform = new Transform(new PointF(0,0), new Size());
            health = 3;
            texture = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\testPlayer.png");
            velocity = (float)4; 
        }
        public void Move(PointF moove)
        {
            transform.position.X += moove.X;
            transform.position.Y += moove.Y;

            transform.rect.Y = transform.position.Y;
            transform.rect.X = transform.position.X;
        }

        public void SetTransform(PointF position)
        {
                transform.position = position;
                transform.size = texture.Size;


            transform.rect.Y = transform.position.Y;
            transform.rect.X = transform.position.X;
        }
    }
}
