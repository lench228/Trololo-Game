namespace Trololo.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Xml.Serialization;
    using Trololo.View;

    public class Level
    {
        public readonly Tile[,] tiles;
        public bool Drawn = false ; 
    

        private Level(Tile[,] tiles)
        {
            this.tiles = tiles;
        }

        public static Level SplitLines(string text)
        {
            var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return LevelCreate(lines);
        }

        public static Level LevelCreate(string[] lines)
        {
            var tiles = new Tile[lines[0].Length, lines.Length];
            var lastTile = new Point(0, 0);
            for (var y = 0; y < lines.Length; y++)
            {
                if (y != 0)
                    lastTile.Y += 140;
                lastTile.X = 0;
                for (var x = 0; x < lines[0].Length; x++)
                {
                    if (x != 0)
                        lastTile.X += 140;
                    switch (lines[y][x])
                    {
                        case 'E':
                            tiles[x, y] = new EnemySpawn(new Point(lastTile.X, lastTile.Y));
                            break;
                        case '.':
                            tiles[x, y] = new EmptyTile(new Point(lastTile.X, lastTile.Y));
                            break;
                        case 'P':
                            tiles[x, y] = new PlayerSpawn(new Point(lastTile.X, lastTile.Y));
                            Game.SetPlayerTransform(new Point(lastTile.X, lastTile.Y)); 
                            break;
                        case 'F':
                            tiles[x, y] = new FloorTile(new Point(lastTile.X, lastTile.Y));
                            break;
                        case 'X':
                            tiles[x, y] = new ExitTile(new Point(lastTile.X, lastTile.Y));
                            break;
                        case 'G':
                            tiles[x, y] = new GuideTile(new Point(lastTile.X, lastTile.Y));
                            break;
                        case 'S':
                            tiles[x, y] = new GunTile(new Point(lastTile.X, lastTile.Y));
                            break;
                        default:
                            tiles[x, y] = new EmptyTile(new Point(lastTile.X, lastTile.Y));
                            break;

                    } 
                }
            }
            return new Level(tiles);
        }
    }
}
