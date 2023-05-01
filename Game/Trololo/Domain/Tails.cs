using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trololo.Domain
{
    public class EnemySpawn : Tile
    {
        public EnemySpawn(Point position)
        {
            IsBorder = false;
            transform = new Transform(position, new RectangleF(position.X, position.Y, 140, 140));
            texture =null;
        }
    }

    public class EmptyTile : Tile
    {
        public EmptyTile(Point position)
        {
            IsBorder = false;
            transform = new Transform(position, new RectangleF(position.X, position.Y, 140, 140));
            texture = null;
        }
    }

    public class FloorTile : Tile
    {
        public FloorTile(Point position)
        {
            IsBorder = true;
            transform = new Transform(position, new RectangleF(position.X, position.Y, 140, 140));
            texture = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\blockTexture.png");
        }
    }

    public class ExitTile : Tile
    {
        public static bool IsEnd;
        public ExitTile(Point position)
        {
            IsEnd = true;
            Name = "Exit"; 
            IsBorder = false;
            transform = new Transform(position, new RectangleF(position.X, position.Y, 140, 140));
            texture = null;
        }
    }
    public class GunTile : Tile
    {
        public GunTile(Point position)
        {
            IsBorder = false; 
            transform = new Transform(position, new RectangleF(position.X, position.Y, 70, 70));
            texture = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\Gun.png");
        }
    }

    public class PlayerSpawn : Tile
    {
        public PlayerSpawn(Point position)
        {

            IsBorder = false; 
            transform = new Transform(position, new RectangleF(position.X, position.Y, 140, 140));
            texture = null;
        }
    }

    public class GuideTile : Tile
    {
        public static int number = 0;
        
        public GuideTile(Point position)
        {
            IsBorder = false; 
            if(number < 4)
                number += 1; 
            transform = new Transform(position, new RectangleF(position.X, position.Y, 140, 140));
            texture = Image.FromFile($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\Ed{number}.png");
        }
    }
}
