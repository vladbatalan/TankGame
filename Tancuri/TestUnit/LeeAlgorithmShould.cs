using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tancuri;
using System.Drawing;
using System.IO;

namespace TestUnit
{
    [TestClass]
    public class LeeAlgoritmShould
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
        public void HaveBordersSet()
        {
            // Execute Lee algorithm
            int[,] result = StrategyUtil.LeeAlgoritm(map.Tiles, new Point(2, 6), new Point(8, 6));

            // Check borders
            for (int i = 0; i <= map.Height + 1; i++)
            {
                Assert.AreEqual(-1, result[i, 0]);
                Assert.AreEqual(-1, result[i, map.Width + 1]);
            }
            for (int j = 0; j <= map.Width + 1; j++)
            {
                Assert.AreEqual(-1, result[0, j]);
                Assert.AreEqual(-1, result[map.Height + 1, j]);
            }
        }


        [TestMethod]
        public void PlaceTheObstaclesOnTheBorder()
        {
            // Execute Lee algorithm
            int[,] result = StrategyUtil.LeeAlgoritm(map.Tiles, new Point(2, 6), new Point(8, 6));

            // Check obstacles
            for (int i = 1; i <= map.Height; i++)
            {
                for (int j = 1; j <= map.Width; j++)
                {
                    if(map.Tiles[i-1, j-1] == 1)
                        Assert.AreEqual(-1, result[i, j]);
                }
            }
        }


        [TestMethod]
        public void WorkOnDefaultMap()
        {
            // Execute Lee algorithm
            int[,] result = StrategyUtil.LeeAlgoritm(map.Tiles, new Point(2, 6), new Point(8, 6));

            int[,] expected = new int[,] {
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
                { -1, -1, -1, 5, 4, 3, 2, 3, 4, -1, -1, -1},
                { -1, -1, 5, 4, 3, 2, 1, 2, 3, 4, -1, -1},
                { -1, 7, 6, 5, -1, -1, -1, -1, 4, 5, 6, -1},
                { -1, 8, 7, -1, -1, 8, 7, 6, 5, 6, 7, -1},
                { -1, 9, 8, 9, 10, 9, 8, -1, -1, 7, 8, -1},
                { -1, 10, 9, 10, 11, 10, 9, -1, -1, 8, 9, -1},
                { -1, 11, 10, -1, -1, 11, 10, 11, 10, 9, 10, -1},
                { -1, 12, 11, 12, -1, -1, -1, -1, 11, 10, 11, -1},
                { -1, -1, 12, 13, 14, 15, 14, 13, 12, 11, -1, -1},
                { -1, -1, -1, 14, 15, 16, 15, 14, 13, -1, -1, -1},
                { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1}
            };

            // Check obstacles
            for (int i = 1; i <= map.Height; i++)
            {
                for (int j = 1; j <= map.Width; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j]);
                }
            }
        }

        [TestMethod]
        public void WorkThenPointsAreClose()
        {
            // Execute Lee algorithm
            int[,] result = StrategyUtil.LeeAlgoritm(map.Tiles, new Point(2, 6), new Point(2, 7));

            int[,] expected = new int[,] {
                  {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
                  {-1, -1, -1, 0, 0, 3, 2, 3, 0, -1, -1, -1},
                  {-1, -1, 0, 0, 0, 2, 1, 2, 0, 0, -1, -1},
                  {-1, 0, 0, 0, -1, -1, -1, -1, 0, 0, 0, -1},
                  {-1, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, -1},
                  {-1, 0, 0, 0, 0, 0, 0, -1, -1, 0, 0, -1},
                  {-1, 0, 0, 0, 0, 0, 0, -1, -1, 0, 0, -1},
                  {-1, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, -1},
                  {-1, 0, 0, 0, -1, -1, -1, -1, 0, 0, 0, -1},
                  {-1, -1, 0, 0, 0, 0, 0, 0, 0, 0, -1, -1},
                  {-1, -1, -1, 0, 0, 0, 0, 0, 0, -1, -1, -1},
                  {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1}
              };

              // Check obstacles
              for (int i = 1; i <= map.Height; i++)
              {
                  for (int j = 1; j <= map.Width; j++)
                  {
                      Assert.AreEqual(expected[i, j], result[i, j]);
                  }
              }
        }


        [TestMethod]
        public void WorkWhenTanksAreOnCenter()
        {
            // Execute Lee algorithm
            int[,] result = StrategyUtil.LeeAlgoritm(map.Tiles, new Point(5, 1), new Point(5, 5));

            int[,] expected = new int[,] {
                    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
                    {-1, -1, -1, 0, 0, 0, 0, 0, 0, -1, -1, -1},
                    {-1, -1, 5, 6, 0, 0, 0, 0, 0, 0, -1, -1},
                    {-1, 3, 4, 5, -1, -1, -1, -1, 0, 0, 0, -1},
                    {-1, 2, 3, -1, -1, 0, 0, 0, 0, 0, 0, -1},
                    {-1, 1, 2, 3, 4, 5, 0, -1, -1, 0, 0, -1},
                    {-1, 2, 3, 4, 5, 0, 0, -1, -1, 0, 0, -1},
                    {-1, 3, 4, -1, -1, 0, 0, 0, 0, 0, 0, -1},
                    {-1, 4, 5, 0, -1, -1, -1, -1, 0, 0, 0, -1},
                    {-1, -1, 0, 0, 0, 0, 0, 0, 0, 0, -1, -1},
                    {-1, -1, -1, 0, 0, 0, 0, 0, 0, -1, -1, -1},
                    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1}
              };

            // Check obstacles
            for (int i = 1; i <= map.Height; i++)
            {
                for (int j = 1; j <= map.Width; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j]);
                }
            }
        }

    }
}
