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
    public class SpriteSheetTests
    {
        [TestMethod()]
        public void GetFacingTest()
        {
            Assert.AreEqual(24, SpriteSheet.GetFacing(32, MathHelper.ToRadians(-5)));
            Assert.AreEqual(24, SpriteSheet.GetFacing(32, MathHelper.ToRadians(0)));
            Assert.AreEqual(24, SpriteSheet.GetFacing(32, MathHelper.ToRadians(5)));
            Assert.AreEqual(20, SpriteSheet.GetFacing(32, MathHelper.ToRadians(40)));
            Assert.AreEqual(20, SpriteSheet.GetFacing(32, MathHelper.ToRadians(45)));
            Assert.AreEqual(20, SpriteSheet.GetFacing(32, MathHelper.ToRadians(50)));
            Assert.AreEqual(16, SpriteSheet.GetFacing(32, MathHelper.ToRadians(85)));
            Assert.AreEqual(16, SpriteSheet.GetFacing(32, MathHelper.ToRadians(90)));
            Assert.AreEqual(16, SpriteSheet.GetFacing(32, MathHelper.ToRadians(95)));
            Assert.AreEqual(12, SpriteSheet.GetFacing(32, MathHelper.ToRadians(130)));
            Assert.AreEqual(12, SpriteSheet.GetFacing(32, MathHelper.ToRadians(135)));
            Assert.AreEqual(12, SpriteSheet.GetFacing(32, MathHelper.ToRadians(140)));
            Assert.AreEqual(8, SpriteSheet.GetFacing(32, MathHelper.ToRadians(175)));
            Assert.AreEqual(8, SpriteSheet.GetFacing(32, MathHelper.ToRadians(180)));
            Assert.AreEqual(8, SpriteSheet.GetFacing(32, MathHelper.ToRadians(185)));
            Assert.AreEqual(4, SpriteSheet.GetFacing(32, MathHelper.ToRadians(220)));
            Assert.AreEqual(4, SpriteSheet.GetFacing(32, MathHelper.ToRadians(225)));
            Assert.AreEqual(4, SpriteSheet.GetFacing(32, MathHelper.ToRadians(230)));
            Assert.AreEqual(0, SpriteSheet.GetFacing(32, MathHelper.ToRadians(265)));
            Assert.AreEqual(0, SpriteSheet.GetFacing(32, MathHelper.ToRadians(270)));
            Assert.AreEqual(0, SpriteSheet.GetFacing(32, MathHelper.ToRadians(275)));
            Assert.AreEqual(28, SpriteSheet.GetFacing(32, MathHelper.ToRadians(310)));
            Assert.AreEqual(28, SpriteSheet.GetFacing(32, MathHelper.ToRadians(315)));
            Assert.AreEqual(28, SpriteSheet.GetFacing(32, MathHelper.ToRadians(320)));
            Assert.AreEqual(24, SpriteSheet.GetFacing(32, MathHelper.ToRadians(355)));
            Assert.AreEqual(24, SpriteSheet.GetFacing(32, MathHelper.ToRadians(360)));
            Assert.AreEqual(24, SpriteSheet.GetFacing(32, MathHelper.ToRadians(365)));
        }
    }
}