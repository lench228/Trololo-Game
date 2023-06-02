using System.Drawing;
using Trololo.View;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Trololo.Properties;
using System;

namespace Trololo.Domain
{
    public class Player : Entity
    {
        public static bool IsWithGun { get; set; }
        public bool IsShooting { get; set; }
        public bool IsInFly { get; set; }

        public bool isOnLadder { get; set; }
        public bool IsInvincible; 

        public List<Bullet> bullets;

        public static double invincibleTime = 5000;
        public static double invincibleCooldown = 20000;

        public Image textureRight; 
        public Image textureLeft;

        public bool isMovingLeft { get; set; }
        public bool isMovingRight { get; set; }
        public bool isJumping { get; set; }
        public bool isMoovingUp { get; set; }
        public bool isMoovingDown { get; set; }


        public Player(int HealthCount)
        {
            SetHealth(HealthCount);

            this.textureRight = Resources.testPlayer;
            this.textureLeft = Resources.testPlayerRotated;

            this.texture = textureRight;

            velocity = (float)10;
            if(Game.currentLevel > 2)
                IsWithGun = true; 
            bullets = new List<Bullet>();
            IsInvincible = false;
        }

        public void SetInvins()
        {
            this.textureRight = Resources.InvinsiblePlayer;
            this.textureLeft =Resources.RotatedInvinsiblePlayer;
            IsInvincible = true; 
        }

        public void UnsetInvins()
        {
            this.textureRight = Resources.testPlayer;
            this.textureLeft = Resources.testPlayerRotated;
            IsInvincible = false;
        }

        public void RotatePlayer(PointF move, Game game)
        {
            if (move.X > 0)
            {
                transform.Direction = 1;
                texture = textureRight; 

            }
            else if (move.X < 0)
            {
                transform.Direction = -1;
                texture = textureLeft; 
            }
        }


        public void UpdatePlayerShots(Game game, List<Bullet> bullets, Dictionary<Enemy, EnemyShoot> toDeleteEnemies, List<Bullet> toDeleteShoots)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var bullet = bullets[i];

                bullet.Shoot();

                if (!CollitionsControl.Collide(bullet.Transform.HitBox, bullet.Transform.Position.X, bullet.Transform.Position.Y, bullet.Transform.HitBox.Width, bullet.Transform.HitBox.Height, game.level.tiles))
                {
                    toDeleteShoots.Add(bullet);
                    bullets.RemoveAt(i);
                    continue;
                }

                CheckEnemies(game, bullets, toDeleteEnemies, toDeleteShoots, i, bullet);
            }
        }

        private static void CheckEnemies(Game game, List<Bullet> bullets, Dictionary<Enemy, EnemyShoot> toDeleteEnemies, List<Bullet> toDeleteShoots, int i, Bullet bullet)
        {
            foreach (var value in game.enemies.Keys)
            {
                if (bullet.Transform.HitBox.IntersectsWith(value.transform.HitBox))
                {
                    value.Hurt();
                    toDeleteShoots.Add(bullet);
                    bullets.RemoveAt(i);

                    if (value.GetHealth() == 0)
                    {
                        toDeleteEnemies[value] = null;
                        if (value.IsDropHeal())
                            game.CreateHeal(value.transform.Position);
                    }
                    break;
                }
            }
        }
    }
}
