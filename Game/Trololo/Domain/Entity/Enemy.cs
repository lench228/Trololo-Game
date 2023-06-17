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
        private readonly int enemyType;
        private int counter = 0;

        public bool isLoopEnd = false;
        public bool isShooted;

        public Enemy()
        {
            this.SetHealth(3);
            this.texture = Image.FromFile("View//Images//EnemySky.png");
            this.velocity = (float)3;
            var rnd = new Random().Next(50);
            enemyType = rnd < 20 ? 0 : 1;
            isShooted = false;
            Transform.Direction = 1; 
        }

        public void UpdateEnemy(Dictionary<Enemy, EnemyShoot> enemies, Game game, Player player)
        {
                var playerPosition = player.Transform.Position;
                var playerHitbox = player.Transform.HitBox;
                var rnd = new Random().Next(100);
                var shoot = enemies[this];

                if (rnd == 40 && !isShooted)
                {
                    GameControl.PlayMedia(GameControl.enemyShootPlayer);
                    shoot.Transform = new Transform(Transform.Position, new RectangleF(Transform.Position.X, Transform.Position.Y, 69, 69));
                    this.isShooted = true;
                    shoot.SetShootDirection(playerPosition.X, playerPosition.Y);
                }
                if (isShooted)
                {
                    var direction = shoot.GetDirection();
                    shoot.Transform.Move(new PointF(direction.X * shoot.velocity, direction.Y * shoot.velocity));

                    if (playerHitbox.IntersectsWith(shoot.Transform.HitBox) && !player.States.IsInvincible)
                    {
                        player.Hurt();
                        isShooted = false;
                    }
                    if (!CollitionsControl.Collide(playerHitbox, shoot.Transform.Position.X, shoot.Transform.Position.Y, 100, 100, game.Level.tiles))
                        isShooted = false;
                }
                if (!isLoopEnd)
                    Patrol(game.Level.tiles);
                else
                    GoGorizontal(game.Level.tiles);
            
        }

        private void Patrol(Tile[,] tiles)
        {
            var move = new PointF(Transform.Direction * velocity * 2, 0);
            if (CollitionsControl.Collide(Transform.HitBox, move.X + Transform.Position.X, move.Y + Transform.Position.Y, Transform.HitBox.Width, Transform.HitBox.Height, tiles))
            {
                Transform.Move(move);
            }
            else
            {
                Transform.Direction = -Transform.Direction;
                counter++;
            }
            if (counter == 2)
            {
                Transform.Direction = 1;
                isLoopEnd = true;
            }
        }

        private void GoGorizontal(Tile[,] tiles)
        {
            if (CollitionsControl.Collide(Transform.HitBox, Transform.Position.X, Transform.Position.Y + 10 * Transform.Direction, Transform.HitBox.Width, Transform.HitBox.Height, tiles))

            Transform.Move(new PointF(0, 6 * Transform.Direction));
            else
            {
                Transform.Direction = -Transform.Direction;
            }
        }

        public bool IsDropHeal()
        {
            return enemyType == 0;  
        }
    }
}
