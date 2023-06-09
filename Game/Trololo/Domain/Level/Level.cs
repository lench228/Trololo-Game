﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

using Trololo.Domain;

namespace Levels
{
    public class Level
    {
        public readonly Tile[,] tiles;
        public readonly List<LadderTile> Ladders = new List<LadderTile>(); 
        public readonly List<ExitTile> ExitTiles = new List<ExitTile>();
        public Tile PlayerSpawn;
        public GunTile GunTile; 
        public Level(string text, Game game, bool flag)
        {
            if (String.IsNullOrEmpty(text))
                tiles = null;
            else
            {
                var stringTiles = SplitLines(text, game);
                tiles = LevelCreate(stringTiles, game);
            }
        }

        public static String[] SplitLines(string text, Game game)
        {
            var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return lines;
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
                            game.CreateEnemy(new Point(lastTile.X, lastTile.Y));
                        break;
                        case '.':
                        tiles[x, y] = new EmptyTile(new Point(lastTile.X, lastTile.Y));
                        break;
                        case 'P':
                            tiles[x, y] = new PlayerSpawn(new Point(lastTile.X, lastTile.Y));
                            game.CreatePlayer(new Point(lastTile.X, lastTile.Y));
                        PlayerSpawn = tiles[x, y];
    

                        break;
                        case 'F':
                        tiles[x, y] = new FloorTile(new Point(lastTile.X, lastTile.Y));
                        break;
                        case 'X':
                        tiles[x, y] = new ExitTile(new Point(lastTile.X, lastTile.Y));
                        ExitTiles.Add(tiles[x, y] as ExitTile);
                        break;
                        case 'G':
                        tiles[x, y] = new GuideTile(new Point(lastTile.X, lastTile.Y));
                        break;
                        case 'S':
                        tiles[x, y] = new GunTile(new Point(lastTile.X, lastTile.Y));
                        GunTile = tiles[x, y] as GunTile; 
                        
                        break;
                        case '#':
                        tiles[x, y] = new LadderTile(new Point(lastTile.X, lastTile.Y));
                        Ladders.Add(tiles[x, y] as LadderTile);
                        break;
                        default:
                        return null; 
                    }
                }
            }
            return tiles;
        }

    }
}
