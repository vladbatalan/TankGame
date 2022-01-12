using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tancuri
{
    public class Map
    {
        public const int MAP_SIZE = 500;

        private int[,] _tiles;

        public int[,] Tiles { set { _tiles = value; TileHeight = MAP_SIZE / _tiles.GetLength(0); TileWidth = MAP_SIZE / _tiles.GetLength(1); } get => _tiles; }
        public int Height => _tiles.GetLength(0);
        public int Width => _tiles.GetLength(1);
        public int TileWidth { set; get; }
        public int TileHeight { set; get; }

        public Map(int height, int width)
        {
            _tiles = new int[height, width];
            TileWidth = MAP_SIZE / width;
            TileHeight = MAP_SIZE / height;
        }

        public Map(string path)
        {
            string[] text = Array.FindAll(File.ReadAllText(path).Split(' ', '\r', '\n'), s => s.Length != 0);

            int height = int.Parse(text[0]);
            int width = int.Parse(text[1]);

            _tiles = new int[height, width];
            TileHeight = MAP_SIZE / height;
            TileWidth = MAP_SIZE / width;

            int index = 2;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _tiles[i, j] = int.Parse(text[index++]);
                }
            }
        }

        public void Paint(Graphics g)
        {
            Rectangle tileRect = new Rectangle();
            tileRect.Size = new Size(TileWidth, TileHeight);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    tileRect.Location = new Point(j * TileWidth, i * TileHeight);

                    if (_tiles[i, j] == 1) {
                        g.FillRectangle(Brushes.Gray, tileRect);
                    }
                }
            }
        }

        /// <summary>
        /// We don't use that anymore
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="obstacleRate"></param>
        /// <returns></returns>
        public static Map RandomGenerateMap(int height, int width, double obstacleRate)
        {
            Map generated = new Map(height, width);

            Random rand = new Random();
            for(int i=0; i < generated.Height; i++)
            {
                for(int j=0; j < generated.Width; j++)
                {

                    generated.Tiles[i, j] = rand.NextDouble() > obstacleRate?0: 1;
                }
            }

            return generated;
        }

    }
}



