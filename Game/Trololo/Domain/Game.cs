using System.Drawing;
using System.IO;

namespace Trololo.Domain
{
    public class Game
    {
        public static Level level;
        public Player player;
        public static Image BackImage; 

        public Game()
        {
            BackImage = new Bitmap("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\background.png");
            level = Level.FromText(File.ReadAllText("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\Domain\\levels.txt"));
        }
    }
}