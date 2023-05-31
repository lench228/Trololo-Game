using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Trololo.View;
using System;
using System.Text;
using System.Collections.Generic;
using Trololo.Properties;

namespace Trololo.Domain
{
    public class Enemy : Entity
    {
        private int enemyType;
        private int direction = 1;
        private int counter = 0;
        public bool isLoopEnd = false;
        public bool isShooted;

        public Enemy(int Type)
        {
            SetHealth(3);
            this.texture = Resources.enemySky;
            this.velocity = (float)5;
            enemyType = Type;
            isShooted = false;
        }

        public static void UpdateEnemy(Dictionary<Enemy, EnemyShoot> enemies, Game game)
        {
            foreach (var enemy in enemies.Keys)
            {
                var rnd = new Random().Next(100);
                var shoot = enemies[enemy];
                if (rnd == 40 && !enemy.isShooted)
                {
                    GameControl.PlayMedia(GameControl.enemyShootPlayer);
                    shoot.transform = new Transform(enemy.transform.GetPosition(), new RectangleF(enemy.transform.position.X, enemy.transform.position.Y, 120, 120));
                    enemy.isShooted = true;
                    shoot.SetShootDirection(game.player.transform.position.X, game.player.transform.position.Y);
                }
                if (enemy.isShooted)
                {
                    var direction = shoot.GetDirection();
                    shoot.transform.Move(new PointF(direction.X * shoot.velocity, direction.Y * shoot.velocity));

                    if (game.player.transform.hitBox.IntersectsWith(shoot.transform.hitBox) && !game.player.IsInvincible)
                    {
                        game.player.Hurt();
                        enemy.isShooted = false;
                    }
                    if (!CollitionsControl.Collide(game.player.transform.hitBox, shoot.transform.position.X, shoot.transform.position.Y, 100, 100, game.level.tiles))
                        enemy.isShooted = false;
                }
                if (!enemy.isLoopEnd)
                    enemy.Patrol(game.level.tiles);
                else
                    enemy.GoGorizontal(game.level.tiles);
            }
        }

        private void Patrol(Tile[,] tiles)
        {

            var move = new PointF(direction * velocity * 2, 0);
            if (CollitionsControl.Collide(transform.hitBox, move.X + transform.position.X, move.Y + transform.position.Y, transform.hitBox.Width, transform.hitBox.Height, tiles))
            {
                transform.Move(move);
            }
            else
            {
                direction = -direction;
                counter++;
            }
            if (counter == 2)
            {
                direction = 1;
                isLoopEnd = true;
            }
        }

        private void GoGorizontal(Tile[,] tiles)
        {
            if (CollitionsControl.Collide(transform.hitBox, transform.position.X, transform.position.Y + 10 * direction, transform.hitBox.Width, transform.hitBox.Height, tiles))
                transform.Move(new PointF(0, 6 * direction));
            else
            {
                direction = -direction;
            }
        }
    }

}
