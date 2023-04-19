namespace Trololo.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Xml.Serialization;

    public class Level
    {
        public readonly Tail[,] tiles;

        private Level(Tail[,] tiles)
        {
            this.tiles = tiles;
        }

        public static Level FromText(string text)
        {
            var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return FromLines(lines);
        }

        public static Level FromLines(string[] lines)
        {
            var tiles = new Tail[lines[0].Length, lines.Length];
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
                            tiles[x, y] = new Tail(lastTile.X, lastTile.Y, default);
                            break;
                        case '.':
                            tiles[x, y] = new Tail(lastTile.X, lastTile.Y, default);
                            break;
                        case 'P':
                            tiles[x, y] = new Tail(lastTile.X, lastTile.Y, default);
                            break;
                        case 'F':
                            tiles[x, y] = new Tail(lastTile.X, lastTile.Y, Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\blockTexture.png"));
                            break;
                        case 'X':
                            tiles[x,y] = new Tail(lastTile.X, lastTile.Y, default);
                            break;
                        case 'G':
                            tiles[x, y] = new Tail(lastTile.X, lastTile.Y, Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\MooveEd.png"));
                            break;
                        default:
                            tiles[x, y] = new Tail(lastTile.X, lastTile.Y, default);
                            break;
                    }
                }
            }

            return new Level(tiles);
        }
    }
}
