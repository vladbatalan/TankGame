using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tancuri;
using System.Drawing;
using System.IO;

namespace TestUnit
{
    [TestClass]
    public class PathFinderShould
    {
        public Map map;

        [TestInitialize]
        public void CreateMap()
        {
            // Get map from resources
            string[] text = Array.FindAll(Resource.map.Split(' ', '\r', '\n'), s => s.Length != 0);
            int height = int.Parse(text[0]);
            int width = int.Parse(text[1]);

            // Create map
            map = new Map(height, width);
            int index = 2;

            // Read all elements
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map.Tiles[i, j] = int.Parse(text[index++]);
                }
            }
        }


        [TestMethod]
        public void WorkOnDefaultMap()
        {

            Point startPoint = new Point(2, 6);
            Point endPoint = new Point(2, 7);

            // Execute Lee algorithm
            int[,] leeMatrix = StrategyUtil.LeeAlgoritm(map.Tiles, startPoint, endPoint);


            Point nextPosition = StrategyUtil.FindDirectionToGo(leeMatrix, startPoint, endPoint);
            Assert.AreEqual(new Point(1, 0), nextPosition);

        }

    }
}
