using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trololo.Domain;

namespace Trololo.Tests
{
    [TestFixture]
    class TransformTest
    {
        [TestCase(0, 0, 1, 1, 10, 10, 1, 1)]
        [TestCase(0, 0, -1, 1, 10, 10, -1, 1)]
        [TestCase(0, 0, 1, -1, 10, 10, 1, -1)]
        [TestCase(0, 0, 0, 0, 10, 10, 0, 0)]

        [Test]
        public void MooveTest(float x, float y, float moveX, float moveY, int sizeX, int sizeY, float expectedX, float expectedY)
        {
            var a = new Transform(new PointF(x, y), new RectangleF(x, y, sizeX, sizeY));
            a.Move(new PointF(moveX, moveY));

            Assert.AreEqual(a.position.X, expectedX); 
            Assert.AreEqual(a.position.Y, expectedY);

            Assert.AreEqual(a.hitBox.X, expectedX);
            Assert.AreEqual(a.hitBox.Y, expectedY);
        }
    }
}