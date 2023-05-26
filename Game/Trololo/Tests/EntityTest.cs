using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trololo.Domain;

namespace Trololo.Tests
{
    [TestFixture]
    class EntityTest
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(-2, 0)]


        [Test]
        public void HealthTest(int health, int expected)
        {
            var a = new Entity();
            a.SetHealth(health);
            Assert.AreEqual(a.GetHealth(), expected);
        }

        [TestCase(1,0,1,0)]
        [TestCase(0,0,0,0)]
        [TestCase(1, 1000, 1, 1000)]

        [Test]
        public void SetTransformTest(float x, float y, float expectedX, float expectedY)
        {
            var a = new Entity();

            a.SetTransform(new System.Drawing.PointF(x, y));

            Assert.AreEqual(a.transform.position.X, expectedX);
            Assert.AreEqual(a.transform.position.Y, expectedY); 
        }
    }
}
