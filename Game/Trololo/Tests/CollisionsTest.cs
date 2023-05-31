using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trololo.Domain;
using Levels; 

namespace Trololo.Tests
{
    [TestFixture]
    class CollisionsTests
    {
        [TestCase(
                "......\n" +
                "......\n" +
                "......\n" +
                "......\n" +
                "......\n", 120, 0 ,true)]

        [TestCase(
                ".....\n" +
                ".....\n" +
                ".FFF.\n" +
                ".....\n" +
                ".....\n", 120, 0 ,false)]

        [TestCase(
                "FFFFFF\n" +
                "F....F\n" +
                "F....F\n" +
                "F....F\n" +
                "F....F\n" + 
                "F....F\n"+
                "FFFFFF\n", 60, 61, true)]

        [TestCase(
                "FFFFFF\n" +
                "F....F\n" +
                "F....F\n" +
                "F....F\n" +
                "F....F\n" +
                "F....F\n" +
                "FFFFFF\n", 60, 120, false)]

        [TestCase(
            "", 0, 0,false)]

        [TestCase(
                "......\n" +
                "......\n" +
                "......\n" +
                "......\n" +
                "......\n", 0, -10, false)]

        [TestCase(
                "......\n" +
                "......\n" +
                "......\n" +
                "......\n" +
                "......\n", -10, 0, false)]

        [TestCase(
                "......\n" +
                "......\n" +
                "......\n" +
                "......\n" +
                "......\n", 301, 0, false)]
        [TestCase(
                "......\n" +
                "......\n" +
                "......\n" +
                "......\n" +
                "......\n", 0, 361, false)]

        [Test]
        public void CollideTest(String level, float playerPositionX, float playerPositionY, bool expected)
        {
            var tiles = new Level(level, new Game(), true).tiles; 
       
            Assert.AreEqual(expected, CollitionsControl.Collide(new RectangleF(playerPositionX, playerPositionX,116, 243), playerPositionX, playerPositionY, 116, 243, tiles));
        }
    }
}
