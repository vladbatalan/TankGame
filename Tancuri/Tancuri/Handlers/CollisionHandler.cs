using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tancuri
{
    public static class CollisionHandler
    {
        public static Map map;

        public static bool CheckCollisionWithMap(Rectangle body)
        {
            // TODO: Translate before rotation to find out the coordonates of the point relative to angle

            // We will split the edges into small divisions to be verified
            int DIVISIONS = 2;

            // Get the size of the division
            float dx = (float)body.Height / DIVISIONS;
            float dy = (float)body.Width / DIVISIONS;

            // Foreach point on the edges check if there is collision
            // Height way
            for (float h = 0; h <= body.Height; h += dx)
            {
                Point leftSidePoint = new Point(
                    body.X,
                    (int)(body.Y + h)
                    );
                Point rightSidePoint = new Point(
                    body.X + body.Width,
                    (int)(body.Y + h)
                    );

                if (CheckPoint(leftSidePoint))
                    return true;

                if (CheckPoint(rightSidePoint))
                    return true;
                
            }

            // Width way
            for (float w = 0; w <= body.Width; w += dy)
            {
                Point upSidePoint = new Point(
                    (int)(body.X + w),
                    body.Y
                    );
                Point downSidePoint = new Point(
                    (int)(body.X + w),
                    body.Y + body.Height
                    );

                if (CheckPoint(upSidePoint))
                    return true;

                if (CheckPoint(downSidePoint))
                    return true;
            }

            return false;
        }

        private static bool CheckPoint(Point pt)
        {
            Rectangle mapBody = new Rectangle(0, 0, Map.MAP_SIZE, Map.MAP_SIZE);

            // Out of map case
            if (!mapBody.Contains(pt))
                return true;

            // Object intersection case
            // Get the square where the point exists and check if it is object or not
            int hIndex = pt.Y / map.TileHeight;
            int wIndex = pt.X / map.TileWidth;

            // Check if it is object
            if (map.Tiles[hIndex, wIndex] != 0)
                return true;

            return false;
        }

        public static PaintUpdateObject GetCollisionObject(PaintUpdateObject targeted)
        {
            // Take all objects existing and verify if they are colliding
            foreach(PaintUpdateObject other in ObjectHandler.AllObjects)
            {
                if(other != targeted)
                {
                    if (other.GetBody().IntersectsWith(targeted.GetBody()))
                        return other;
                }
            }

            return null;
        }
    }
}
