using System;
using System.Drawing; 

namespace Trololo.Domain
{
    public class Projectile
    {
        public Transform Transform { get; set; }
        public Image Texture { get; set; }
        public float velocity { get; set; }

       public Projectile(Image projectTexture, PointF position)
       {
            velocity = (float)2.5;
            Texture = projectTexture;
            Transform = new Transform(position, new RectangleF(position.X, position.Y, Texture.Size.Width, Texture.Size.Height));
       }
    }
}
