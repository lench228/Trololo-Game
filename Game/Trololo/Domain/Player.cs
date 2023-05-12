using System.Drawing;
using Trololo.View;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Data;
using System.Collections.Generic;

namespace Trololo.Domain
{
    public class Player : Entity
    {
        public static bool IsWithGun { get; set; }
        public bool IsShooting { get; set; }

        public List<Bullet> bullets; 

        public Player(int HealthCount)
        {
            SetHealth(HealthCount);
            this.texture = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\testPlayer.png");
            velocity = (float)4;
            if(Game.currentLevel > 2)
                IsWithGun = true; 
            bullets = new List<Bullet>();
        }


    }
}
