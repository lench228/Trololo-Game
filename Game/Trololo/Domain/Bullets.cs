using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trololo.Domain
{
    public class Bullet : Projectile
    {
        private int direction; 
        public Bullet(Image projectTexture, PointF position, int Direction): base(projectTexture, position) 
        { 
            direction = Direction;
            transform.position.Y += 80; 
        }

        public void Shoot()
        {
           transform.Move(new PointF(velocity * direction * 4, 0));  
        }
    }
}
