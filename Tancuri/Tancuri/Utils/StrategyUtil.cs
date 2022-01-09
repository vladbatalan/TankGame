using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tancuri
{
    public class StrategyUtil
    {
        public static Point[] directions = new Point[] {
                new Point(-1, 0),    // UP            
                new Point(0,  1),    // Right            
                new Point(1,  0),    // DOWN            
                new Point(0, -1)     // LEFT            
            };

        /// <summary>
        /// Function responible for returning a diraction to take in order to ajust to the targeted angle
        /// </summary>
        /// <param name="targetAngle"> The angle that needs to be reached </param>
        /// <param name="normalizedAngle"> The current angle </param>
        /// <returns> -1 if a left turn is needed, 1 if a right turn is needed</returns>
        public static int AjustRotation(double targetAngle, double normalizedAngle)
        {
            double difference = normalizedAngle - targetAngle;
            double leftDistance, rightDistance;

            if (difference < 0)
            {
                // The left direction is main
                leftDistance = -difference;
                rightDistance = 360 - leftDistance;
            }
            else
            {
                // The right direction is main
                rightDistance = difference;
                leftDistance = 360 - rightDistance;
            }

            // Ajust rotation
            if (leftDistance >= rightDistance)
                return -1;
            else
                return  1;
        }

        /// <summary>
        /// Function responsible for calculating the angle of a line represented by two points
        /// </summary>
        /// <param name="target"></param>
        /// <param name="currentPosition"></param>
        /// <returns></returns>
        public static double EvaluateAngleBetweenPoints(Point target, Point currentPosition)
        {
            Point lineVector = new Point(target.X - currentPosition.X, target.Y - currentPosition.Y);
            Point versor = new Point(1, 0);

            double dotProduct = lineVector.X * versor.X + lineVector.Y * versor.Y;
            double module = Math.Sqrt(lineVector.X * lineVector.X + lineVector.Y * lineVector.Y);

            double targetedAngle = Math.Acos(dotProduct / module);

            // To degrees
            targetedAngle = ((180.0 / Math.PI) * targetedAngle) % 360;

            if (targetedAngle < 0) targetedAngle += 360;

            // Check position and flip angle if needed
            if (target.Y < currentPosition.Y)
                targetedAngle = 360 - targetedAngle;

            return targetedAngle;
        }


        /// <summary>
        /// Function responsible for evaluating the shortest path to the enemyIndex.
        /// </summary>
        /// <param name="mapMatrix"> The position of the obstacles on the map </param>
        /// <param name="tankIndexes"> The starting point of the algorithm </param>
        /// <param name="enemyIndexes"> The target point of the algorithm </param>
        /// <returns> The resulting matrix </returns>
        public static int[,] LeeAlgoritm(int[,] mapMatrix, Point tankIndexes, Point enemyIndexes)
        {
            int height = mapMatrix.GetLength(0);
            int width = mapMatrix.GetLength(1);

            // Get the bordered matrix on which Lee algorithm will be performed
            // -1 = obstacle
            int[,] border = new int[height + 2, width + 2];
            // Ajust the borders of the matrix
            //
            // -1 -1 -1 -1 -1
            // -1          -1
            // -1          -1
            // -1 -1 -1 -1 -1

            for (int i = 1; i <= height; i++)
                for (int j = 1; j <= width; j++)
                    border[i, j] = mapMatrix[i - 1, j - 1] == 1 ? -1 : 0;

            for (int i = 0; i <= height + 1; i++)
                border[i, 0] = border[i, width + 1] = -1;
            for (int j = 0; j <= width + 1; j++)
                border[0, j] = border[height + 1, j] = -1;

            // Get the queue
            Queue<Point> toBeVisited = new Queue<Point>();

            // Push the starting point
            toBeVisited.Enqueue(tankIndexes);

            // Set the initial distance as 1
            border[tankIndexes.X, tankIndexes.Y] = 1;

            // While there are points to be visited
            while (toBeVisited.Count != 0)
            {
                // Get the first in the queue
                Point currentPoint = toBeVisited.Dequeue();

                // If it is the enemy, we found the path, we can stop
                if (currentPoint.Equals(enemyIndexes))
                    break;

                // Foreach neighbour, check if it can be visited
                foreach (Point delta in directions)
                {
                    // Get the neigbour by incrementing the position with delta
                    Point neighbour = new Point(currentPoint.X + delta.X, currentPoint.Y + delta.Y);

                    // If it can be visited
                    //      - it has border = 0 (it was'n visited before and it is not margin)
                    //      - there is no obstacle on the map
                    if (border[neighbour.X, neighbour.Y] == 0 && mapMatrix[neighbour.X - 1, neighbour.Y - 1] != 1)
                    {
                        // Update the border
                        border[neighbour.X, neighbour.Y] = border[currentPoint.X, currentPoint.Y] + 1;

                        // Add the neighbour to the queue
                        toBeVisited.Enqueue(neighbour);
                    }
                }
            }

            return border;
        }


        /// <summary>
        /// Method responsible for evaluating the direction to take in order to move on a path 
        /// </summary>
        /// <param name="leeMatrix"> The border matrix returned by the Lee algoritm </param>
        /// <param name="startPosition"> The starting position </param>
        /// <param name="finishPosition"> The ending position </param>
        /// <returns> A direction described by a Point from the directions vector </returns>
        public static Point FindDirectionToGo(int[,] leeMatrix, Point startPosition, Point finishPosition)
        {
            // Obtain the list with the path
            Point lastPoint = finishPosition;
            Point current = finishPosition;

            // While not in the player place
            while (!current.Equals(startPosition))
            {
                // Get the anterior position by checking all neighbours
                int nextX = -10, nextY = -10;
                Point nextPoint;
                foreach (Point delta in directions)
                {
                    // Get the neigbour by incrementing the position with delta
                    nextPoint = new Point(current.X + delta.X, current.Y + delta.Y);

                    // The difference between 2 adiacent points in path is 1
                    if (leeMatrix[nextPoint.X, nextPoint.Y] - leeMatrix[current.X, current.Y] == -1)
                    {
                        nextX = nextPoint.X;
                        nextY = nextPoint.Y;
                        break;
                    }
                }

                // This sequence cannot be activated
                if (nextX == -10 || nextY == -10)
                    throw new Exception("The path could not be reconstructed!");

                // lastPoint = currentPoint selected
                lastPoint = current;
                current = new Point(nextX, nextY);
            }

            // This element is the next position
            Point nextPosition = lastPoint;

            // Extract the exact coordinates on the map
            return new Point(
                 nextPosition.Y - startPosition.Y,
                 nextPosition.X - startPosition.X
                );
        }
    }
}
