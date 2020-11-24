using Microsoft.VisualStudio.TestTools.UnitTesting;
using TiberianStrike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TiberianStrike.Tests
{
    [TestClass()]
    public class VectorHelpersTests
    {
        [TestMethod()]
        public void GetRotationToFaceTest()
        {
            Assert.AreEqual(null,                    VectorHelpers.GetRotationToFace(Vector2.Zero, new Vector2(0, 0)));
            Assert.AreEqual(MathHelper.PiOver4,      VectorHelpers.GetRotationToFace(Vector2.Zero, new Vector2(1, 1)).Value, 0.01);
            Assert.AreEqual(MathHelper.PiOver2,      VectorHelpers.GetRotationToFace(Vector2.Zero, new Vector2(0, 1)).Value, 0.01);
            Assert.AreEqual(3 * MathHelper.PiOver4,  VectorHelpers.GetRotationToFace(Vector2.Zero, new Vector2(-1, 1)).Value, 0.01);
            Assert.AreEqual(MathHelper.Pi,           VectorHelpers.GetRotationToFace(Vector2.Zero, new Vector2(-1, 0)).Value, 0.01);
            Assert.AreEqual(-3 * MathHelper.PiOver4, VectorHelpers.GetRotationToFace(Vector2.Zero, new Vector2(-1, -1)).Value, 0.01);
            Assert.AreEqual(-1 * MathHelper.PiOver2, VectorHelpers.GetRotationToFace(Vector2.Zero, new Vector2(0, -1)).Value, 0.01);
            Assert.AreEqual(-1 * MathHelper.PiOver4, VectorHelpers.GetRotationToFace(Vector2.Zero, new Vector2(1, -1)).Value, 0.01);
        }

        [TestMethod()]
        public void GetVectorInDirectionTest()
        {
            Assert.AreEqual(new Vector2(1, 0),   VectorHelpers.GetVectorInDirection(0));
            Assert.AreEqual(new Vector2(1, 1),   VectorHelpers.GetVectorInDirection(MathHelper.PiOver4));
            Assert.AreEqual(new Vector2(0, 1),   VectorHelpers.GetVectorInDirection(MathHelper.PiOver2));
            Assert.AreEqual(new Vector2(-1, 1),  VectorHelpers.GetVectorInDirection(3 * MathHelper.PiOver4));
            Assert.AreEqual(new Vector2(-1, 0),  VectorHelpers.GetVectorInDirection(MathHelper.Pi));
            Assert.AreEqual(new Vector2(-1, -1), VectorHelpers.GetVectorInDirection(-3 * MathHelper.PiOver4));
            Assert.AreEqual(new Vector2(0, -1),  VectorHelpers.GetVectorInDirection(-1 * MathHelper.PiOver2));
            Assert.AreEqual(new Vector2(1, -1), VectorHelpers.GetVectorInDirection(-1 * MathHelper.PiOver4));
        }
    }
}