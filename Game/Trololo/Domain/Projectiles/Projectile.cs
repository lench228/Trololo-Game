using System;
using System.Drawing; 

namespace Trololo.Domain
{
    public class Projectile
    {
        public Transform transform { get; set; }
        public Image texture;
        public float velocity;

       public Projectile(Image projectTexture, PointF position)
       {
            velocity = (float)2.5;
            texture = projectTexture;
            transform = new Transform(position, new RectangleF(position.X, position.Y, texture.Size.Width, texture.Size.Height));
       }

    }
}
