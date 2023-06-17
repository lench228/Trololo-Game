using System.Drawing;
using Trololo.View;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Trololo.Properties;
using System;
using System.Linq.Expressions;

namespace Trololo.Domain
{
    public class PlayerStates 
    {
        public PlayerStates() 
        { 
            isMoovingDown= false;
            isMoovingUp= false;
            isMovingLeft= false;
            isMovingRight= false;
            IsWithGun= false;
            IsShooting = false;
            IsInFly = true;
            isOnLadder = false;
            IsInvincible= false;
        }

        public bool isMovingLeft { get; set; }
        public bool isMovingRight { get; set; }
        public bool isJumping { get; set; }
        public bool isMoovingUp { get; set; }
        public bool isMoovingDown { get; set; }
        public bool IsWithGun { get; set; }
        public bool IsShooting { get; set; }
        public bool IsInFly { get; set; }
        public bool isOnLadder { get; set; }
        public bool IsInvincible { get; set; }
    }
    public class Player : Entity
    {
        public PlayerStates States; 
        public List<Bullet> bullets;

        public static double invincibleTime = 5000;
        public static double invincibleCooldown = 20000;

        public Image textureRight; 
        public Image textureLeft;


        public Player(int HealthCount)
        {
            SetHealth(HealthCount);

            States = new PlayerStates();

            this.textureRight = Image.FromFile("View//Images//testPlayer.png");
            this.textureLeft = Image.FromFile("View//Images//testPlayerRotated.png");

            this.texture = textureRight;

            velocity = (float)10;
            if(Game.CurrentLevel > 2)
                States.IsWithGun = true; 
            bullets = new List<Bullet>();
            States.IsInvincible = false;
        }

        public void SetInvins()
        {
            this.textureRight = Image.FromFile("View//Images//InvinsiblePlayer.png");
            this.textureLeft = Image.FromFile("View//Images//RotatedInvinsiblePlayer.png");
            States.IsInvincible = true; 
        }

        public void UnsetInvins()
        {
            this.textureRight = Image.FromFile("View//Images//testPlayer.png");
            this.textureLeft = Image.FromFile("View//Images//testPlayerRotated.png");
            States.IsInvincible = false;
        }

        public void RotatePlayer(PointF move, Game game)
        {
            if (move.X > 0)
            {
                Transform.Direction = 1;
                texture = textureRight; 

            }
            else if (move.X < 0)
            {
                Transform.Direction = -1;
                texture = textureLeft; 
            }
        }


        public void UpdatePlayerShots(Game game, List<Bullet> bullets, Dictionary<Enemy, EnemyShoot> toDeleteEnemies, List<Bullet> toDeleteShoots)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var bullet = bullets[i];

                bullets[i].Shoot();

                if (!CollitionsControl.Collide(bullet.Transform.HitBox, bullet.Transform.Position.X, bullet.Transform.Position.Y, bullet.Transform.HitBox.Width, bullet.Transform.HitBox.Height, game.Level.tiles))
                {
                    bullets.RemoveAt(i);
                    continue;
                }
                CheckEnemies(game, bullets, toDeleteEnemies, toDeleteShoots, i, bullet);
            }
        }

        private static void CheckEnemies(Game game, List<Bullet> bullets, Dictionary<Enemy, EnemyShoot> toDeleteEnemies, List<Bullet> toDeleteShoots, int i, Bullet bullet)
        {
            foreach (var value in game.Enemies.Keys)
            {
                if (bullet.Transform.HitBox.IntersectsWith(value.Transform.HitBox))
                {
                    value.Hurt();
                    toDeleteShoots.Add(bullet);
                    bullets.RemoveAt(i);

                    if (value.GetHealth() == 0)
                    {
                        toDeleteEnemies[value] = null;
                        if (value.IsDropHeal())
                            game.CreateHeal(value.Transform.Position);
                    }
                    break;
                }
            }
        }
    }
}
