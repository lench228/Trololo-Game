using NUnit.Framework;
using System;
using System.Drawing;
using Trololo.Domain;

namespace Trololo.Tests
{
    [TestFixture]
    class LevelTests
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(" k")]
        [TestCase("asjawerj  jkas")]
        [Test]
        public void LevelIsNull(String levelTxt)
        {
            var level = new Level(levelTxt, new Game());
            Assert.IsNull(level.tiles);
        }


        [Test]
        public void CorrectTileLocation()
        {
            var result = new Tile[2, 2]{{new FloorTile(new System.Drawing.Point(0, 0)), new FloorTile(new System.Drawing.Point(0,60))},
            { new FloorTile(new System.Drawing.Point(60, 0)), new FloorTile(new System.Drawing.Point(60, 60)) } };

            var level = new Level("FF\nFF", new Game());
            CompareLevel(result, level);
        }

        [Test]
        public void CorrectTileLocation2()
        {
            var result = new Tile[2, 4]
            {
                {
                    new FloorTile(new System.Drawing.Point(0, 0)),
                    new FloorTile(new System.Drawing.Point(0,60)),
                    new FloorTile(new System.Drawing.Point(0, 120)),
                    new FloorTile(new System.Drawing.Point(0,180))

                },
            {
                    new FloorTile(new System.Drawing.Point(60, 0)),
                    new FloorTile(new System.Drawing.Point(60, 60)),
                    new FloorTile(new System.Drawing.Point(60, 120)),
                    new FloorTile(new System.Drawing.Point(60, 180))
                }
            };

            var level = new Level("FFFF\nFFFF", new Game());
            CompareLevel(result, level);
        }

        private static void CompareLevel(Tile[,] result, Level level)
        {
            for (var i = 0; i < level.tiles.GetLength(0); i++)
                for (var j = 0; j < level.tiles.GetLength(1); j++)
                {
                    var tile1transform = level.tiles[i, j].transform.position;
                    var tiles2transform = result[i, j].transform.position;

                    Assert.AreEqual(tile1transform.X, tiles2transform.X);
                    Assert.AreEqual(tile1transform.Y, tiles2transform.Y);
                }
        }
    }
}
