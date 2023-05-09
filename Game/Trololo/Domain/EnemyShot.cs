using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trololo.Domain
{
    public class EnemyShoot : Projectile
    {
       private PointF playerPos;
       private bool IsTargetLost = false; 
      
        public EnemyShoot(Image projectTexture, PointF position, Player player) : base(projectTexture, position) 
        {
            playerPos = player.transform.position;
        }
        public void Shoot()
        {
            var playerLoc = new PointF(playerPos.X, playerPos.Y);
            var MoveTo = new PointF();

            if(!IsTargetLost)
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

            if ((int)transform.position.X == (int)playerLoc.X && (int)transform.position.Y+1 == (int)playerLoc.Y-3)
            {
                IsTargetLost = true;
                
            }

            if (IsTargetLost)
                MoveTo.X += velocity * 2;

            transform.Move(MoveTo);
        }

        public void UpdatePlayerPos(PointF pos)
        {
            playerPos = pos;
            playerPos.Y += 160;
        }
    }
}
