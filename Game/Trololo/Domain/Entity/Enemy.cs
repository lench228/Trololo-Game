using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Trololo.View;
using System;
using System.Text;
using System.Collections.Generic;
using Trololo.Properties;
using System.Windows.Forms;

namespace Trololo.Domain
{
    public class Enemy : Entity
    {
        private int enemyType;
        private int counter = 0;

        public bool isLoopEnd = false;
        public bool isShooted;

        public Enemy(int Type)
        {
            this.SetHealth(3);
            this.texture = Resources.enemySky;
            this.velocity = (float)5;
            var rnd = new Random().Next(100);
            enemyType = rnd < 10 ? 0 : 1;
            isShooted = false;
            transform.Direction = 1; 
        }

        public void UpdateEnemy(Dictionary<Enemy, EnemyShoot> enemies, Game game, Player player)
        {
                var playerPosition = player.transform.Position;
                var playerHitbox = player.transform.HitBox;
                var rnd = new Random().Next(100);
                var shoot = enemies[this];

                if (rnd == 40 && !this.isShooted)
                {
                    GameControl.PlayMedia(GameControl.enemyShootPlayer);
                    shoot.Transform = new Transform(this.transform.Position, new RectangleF(this.transform.Position.X, this.transform.Position.Y, 100, 100));
                    this.isShooted = true;
                    shoot.SetShootDirection(playerPosition.X, playerPosition.Y);
                }
                if (this.isShooted)
                {
                    var direction = shoot.GetDirection();
                    shoot.Transform.Move(new PointF(direction.X * shoot.velocity, direction.Y * shoot.velocity));

                    if (playerHitbox.IntersectsWith(shoot.Transform.HitBox) && !player.IsInvincible)
                    {
                        player.Hurt();
                        this.isShooted = false;
                    }
                    if (!CollitionsControl.Collide(playerHitbox, shoot.Transform.Position.X, shoot.Transform.Position.Y, 100, 100, game.level.tiles))
                        this.isShooted = false;
                }
                if (!this.isLoopEnd)
                    this.Patrol(game.level.tiles);
                else
                    this.GoGorizontal(game.level.tiles);
            
        }

        private void Patrol(Tile[,] tiles)
        {
            var move = new PointF(transform.Direction * velocity * 2, 0);
            if (CollitionsControl.Collide(this.transform.HitBox, move.X + transform.Position.X, move.Y + transform.Position.Y, this.transform.HitBox.Width, this.transform.HitBox.Height, tiles))
            {
                transform.Move(move);
            }
            else
            {
                transform.Direction = -transform.Direction;
                counter++;
            }
            if (counter == 2)
            {
                transform.Direction = 1;
                isLoopEnd = true;
            }
        }

        private void GoGorizontal(Tile[,] tiles)
        {
            if (CollitionsControl.Collide(this.transform.HitBox, transform.Position.X, this.transform.Position.Y + 10 * transform.Direction, this.transform.HitBox.Width, this.transform.HitBox.Height, tiles))

                transform.Move(new PointF(0, 6 * transform.Direction));
            else
            {
                transform.Direction = -transform.Direction;
            }
        }

        public bool IsDropHeal()
        {
            return enemyType == 0 ? true : false;  
        }
    }

}
