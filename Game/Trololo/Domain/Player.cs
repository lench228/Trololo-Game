using System.Drawing;
using Trololo.View;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Data;

namespace Trololo.Domain
{
    public class Player : Entity
    {
        public Player()
        {
            health = 3;
            this.texture = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\testPlayer.png");
            velocity = (float)4; 
        }
    }
}
