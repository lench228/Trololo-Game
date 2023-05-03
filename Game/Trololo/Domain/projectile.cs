using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trololo.Domain
{
    public class Projectile
    {
       public Transform transform { get; set; } 
       public readonly Image texture;
        private float velocity = (float)2.5; 
       
       
       public Projectile(Image projectTexture, PointF position)
       {
            texture = projectTexture;
            transform = new Transform(position, new RectangleF(position.X, position.Y, texture.Size.Width, texture.Size.Height));
       }


        public void Shoot(Player player)
        {
            var playerLoc = new PointF(player.transform.position.X, player.transform.position.Y);
            var MoveTo = new PointF();

            if (transform.position.X > playerLoc.X)
            {
                MoveTo.X -= velocity * 2;
            }

            if (transform.position.X < playerLoc.X)
            {
                MoveTo.X += velocity * 2;
            }

            if (transform.position.Y > playerLoc.Y)
                MoveTo.Y -= velocity * 2;
            if (transform.position.Y < playerLoc.Y)
                MoveTo.Y += velocity * 2;

            transform.Move(MoveTo);
        }



    }
}
