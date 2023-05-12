﻿namespace Trololo.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Xml.Serialization;
    using Trololo.View;

    public class Level
    {
        public readonly Tile[,] tiles;
        public Dictionary<EnemySky, EnemyShoot> enemies = new Dictionary<EnemySky, EnemyShoot>();

        public Level(string text, Game game)
        {
            if (String.IsNullOrEmpty(text))
                tiles = null; 
            else 
                 tiles = SplitLines(text, game);
        }

        private Tile[,] SplitLines(string text, Game game)
        {
            var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return LevelCreate(lines, game);
        }

        public Tile[,] LevelCreate(string[] lines, Game game)
        {
            var tiles = new Tile[lines[0].Length, lines.Length];
            var lastTile = new Point(0, 0);
            for (var y = 0; y < lines.Length; y++)
            {
                if (y != 0)
                    lastTile.Y += 60;
                lastTile.X = 0;
                for (var x = 0; x < lines[0].Length; x++)
                {
                    if (x != 0)
                        lastTile.X += 60;
                    switch (lines[y][x])
                    {
                        case 'E':
                        tiles[x, y] = new EnemySpawn(new Point(lastTile.X, lastTile.Y));

                        this.CreateEnemy(new Point(lastTile.X, lastTile.Y), 0, game);
                        break;
                        case '.':
                        tiles[x, y] = new EmptyTile(new Point(lastTile.X, lastTile.Y));
                        break;
                        case 'P':
                        tiles[x, y] = new PlayerSpawn(new Point(lastTile.X, lastTile.Y));
                        game.CreatePlayer(new Point(lastTile.X, lastTile.Y));
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
                        return null; 

                    }
                }
            }
            return tiles;
        }

        private void CreateEnemy(Point x, int type, Game game)
        {
            var enemy = new EnemySky(type);
            enemies[enemy] = new EnemyShoot(Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\EnemyShot.png"), enemy.transform.position, game.player);
            enemy.SetTransform(x);
        }
    }
}
