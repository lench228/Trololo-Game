using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trololo.Properties;

namespace Trololo.Domain
{
    public class EnemySpawn : Tile
    {
        public EnemySpawn(Point position)
        {
            IsBorder = false;
            Transform = new Transform(position, new RectangleF(position.X, position.Y, 60, 60));
            Texture =null;
        }
    }

    public class EmptyTile : Tile
    {
        public EmptyTile(Point position)
        {
            IsBorder = false;
            Transform = new Transform(position, new RectangleF(position.X, position.Y, 60, 60));
            Texture = null;
        }
    }

    public class FloorTile : Tile
    {
        public FloorTile(Point position)
        {
            IsBorder = true;
            Transform = new Transform(position, new RectangleF(position.X, position.Y, 60, 60));
            Texture = Resources.blockTexture;
        }
    }

    public class ExitTile : Tile
    {
        public static bool IsEnd;
        public ExitTile(Point position)
        {
            IsBorder = false;
            Transform = new Transform(position, new RectangleF(position.X, position.Y, 60, 60));
            Texture = null;
        }
    }
    public class GunTile : Tile
    {
        public static bool IsGunTile ;
        public GunTile(Point position)
        {
            IsBorder = false;
            IsGunTile = true; 

            Texture = Resources.gun;
            Transform = new Transform(position, new RectangleF(position.X, position.Y, Texture.Size.Width, Texture.Size.Width));
        }
    }

    public class PlayerSpawn : Tile
    {
        public PlayerSpawn(Point position)
        {
            IsBorder = false; 
            Transform = new Transform(position, new RectangleF(position.X, position.Y, 60, 60));
            Texture = null;
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
            Transform = new Transform(position, new RectangleF(position.X, position.Y, 140, 140));
            Texture = Image.FromFile($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Images\\Ed{number}.png");
        }
    }

    public class LadderTile : Tile
    {

        public LadderTile(Point position)
        {
            IsBorder = false;
            Transform = new Transform(position, new RectangleF(position.X, position.Y, 60, 60));
            Texture = Resources.Ladder;
        }
    }
}
