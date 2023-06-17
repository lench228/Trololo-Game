using NUnit.Framework;
using System;
using System.Drawing;
using Trololo.Domain;
using Levels;

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
            var level = new Level(levelTxt, new Game(), true);
            Assert.IsNull(level.tiles);
        }


        [TestCase("", new String[0])]
        [TestCase("ss", new String[1] {"ss"})]
        [TestCase("beb\nbeb", new String[2] {"beb", "beb"})]
        [TestCase("\n\n\nd", new String[1] { "d" })]
        [TestCase("O\nL\nD", new String[3] { "O", "L", "D" })]


        [Test]
        public static void SplitLinesTests(string text, string[] expected)
        {
            var a = Level.SplitLines(text, new Game()); 
            Assert.AreEqual(expected, a);
        }

        [Test]
        public void CorrectTileLocation()
        {
            var result = new Tile[2, 2]{{new FloorTile(new System.Drawing.Point(0, 0)), new FloorTile(new System.Drawing.Point(0,60))},
            { new FloorTile(new System.Drawing.Point(60, 0)), new FloorTile(new System.Drawing.Point(60, 60)) } };

            var level = new Level("FF\nFF", new Game(), true);
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

            var level = new Level("FF\nFF\nFF\nFF", new Game(), true);
            CompareLevel(result, level);
        }

        private static void CompareLevel(Tile[,] result, Level level)
        {
            for (var i = 0; i < level.tiles.GetLength(0); i++)
                for (var j = 0; j < level.tiles.GetLength(1); j++)
                {
                    var tile1transform = level.tiles[i, j].Transform.Position;
                    var tiles2transform = result[i, j].Transform.Position;

                    Assert.AreEqual(tile1transform.X, tiles2transform.X);
                    Assert.AreEqual(tile1transform.Y, tiles2transform.Y);
                }
        }
    }
}
