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
            Transform.Position = new PointF(Transform.Position.X, Transform.Position.Y + 80); 
        }

        public void Shoot()
        {
           Transform.Move(new PointF(velocity * direction * 4, 0));  
        }
    }
}
