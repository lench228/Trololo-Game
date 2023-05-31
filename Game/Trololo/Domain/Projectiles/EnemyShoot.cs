using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trololo.Domain
{
    public class EnemyShoot : Projectile
    {
        private PointF playerPos;
        private float directionX; 
        private float directionY;
      
        public EnemyShoot(Image projectTexture, PointF position, Player player) : base(projectTexture, position) 
        {
            playerPos = player.transform.position;
            this.velocity = 5;
        }
        public void SetShootDirection(float playerX, float playerY)
        {
            directionX = playerX - transform.position.X;
            directionY = playerY - transform.position.Y;

            var length = (float)Math.Sqrt(directionX * directionX + directionY * directionY);
            directionX /= length;
            directionY /= length;
        }

        public PointF GetDirection()
        {
            return new PointF(directionX, directionY); 
        }
    }
}
