using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tancuri;
using System.Drawing;
using System.IO;

namespace TestUnit
{
    [TestClass]
    public class AjustRotationShould
    {
        private const int LEFT = -1;
        private const int RIGHT = 1;

        [TestMethod]
        public void DetectWhenToTurnLeft()
        {
            double target = 80;
            double reference = 120;

            int ajustmentDirection = StrategyUtil.AjustRotation(target, reference);
            Assert.AreEqual(LEFT, ajustmentDirection);
        }

        [TestMethod]
        public void DetectWhenToTurnRight()
        {
            double target = 0;
            double reference = 270;

            int ajustmentDirection = StrategyUtil.AjustRotation(target, reference);
            Assert.AreEqual(RIGHT, ajustmentDirection);
        }

        [TestMethod]
        public void DetectForAllAngles()
        {
            double target = 0;

            for(double reference = 1; reference <= 359; reference++)
            {
                int direction = reference > 180 ? RIGHT : LEFT;

                int ajustmentDirection = StrategyUtil.AjustRotation(target, reference);
                Assert.AreEqual(direction, ajustmentDirection);
            }
        }
        [TestMethod]
        public void DetectWhenAnglesAreEqual()
        {
            double target = 0;
            double reference = 0;
               
            int ajustmentDirection = StrategyUtil.AjustRotation(target, reference);
            Assert.AreEqual(LEFT, ajustmentDirection);
        }
    }
}
