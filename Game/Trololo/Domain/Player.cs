using System.Drawing;
using Trololo.View;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Data;
using System.Collections.Generic;
using System.Linq; 

namespace Trololo.Domain
{
    public class Player : Entity
    {
        public static bool IsWithGun { get; set; }
        public bool IsShooting { get; set; }

        public bool IsJumping { get; set; }

        public bool IsInvincible; 

        public List<Bullet> bullets;

        public static double invincibleTime = 5000;
        public static double invincibleCooldown = 20000;

        public Image textureRight; 
        public Image textureLeft;

        public Player(int HealthCount)
        {
            SetHealth(HealthCount);

            this.textureRight = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\testPlayer.png");
            this.textureLeft = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\TestPlayerRotated.png");

            this.texture = textureRight;

            velocity = (float)10;
            if(Game.currentLevel > 2)
                IsWithGun = true; 
            bullets = new List<Bullet>();
            IsInvincible = false;
        }

        public void SetInvins()
        {
            this.textureRight = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\InvinsiblePlayer.png");
            this.textureLeft = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\RotatedInvinsiblePlayer.png");
            IsInvincible = true; 
        }

        public void UnsetInvins()
        {
            this.textureRight = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\testPlayer.png");
            this.textureLeft = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\TestPlayerRotated.png");
        }
        public void RotatePlayer(PointF move, Game game)
        {
            var dir = game.player.transform.Direction;

            if (move.X > 0)
            {
                game.player.transform.Direction = 1;
                game.player.texture = textureRight; 

            }
            else if (move.X < 0)
            {
                game.player.transform.Direction = -1;
                game.player.texture = textureLeft; 
            }
        }


    }
}
