using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tancuri
{
    public class ComputerController : KeyboardController
    {
        private const int SHOOT_PAUSE_MILISECONDS = 500;
        private int timeSinceShoot = 0;

        private Tuple<int, int>[] neighbours = new Tuple<int, int>[] {
                new Tuple<int, int>(1,3),  // Neighbours of 0
                new Tuple<int, int>(2,0),  // Neighbours of 1
                new Tuple<int, int>(3,1),  // Neighbours of 2
                new Tuple<int, int>(0,2)   // Neighbours of 3
            };

        // Direction vector
        private Point[] directions = new Point[] {
                new Point(-1, 0),    // UP            
                new Point(0,  1),    // Right            
                new Point(1,  0),    // DOWN            
                new Point(0, -1)     // LEFT            
            };

        public ComputerController(Tank tank)
        {
            ControlledTank = tank;
        }

        public override void KeyDown(KeyEventArgs e){}

        public override void KeyUp(KeyEventArgs e){}

        public override void Update()
        {

            // Update cannon rotation
            Tank enemy = ObjectHandler.GetEnemyOfTank(ControlledTank);

            // Cannon follows the enemy
            bool isCannonAlligned = CannonFollowEnemy(enemy);

            // Update time since last shoot
            timeSinceShoot = Math.Min(timeSinceShoot + 33, SHOOT_PAUSE_MILISECONDS);

            // See obstacle in the path of player, if there are not, shoot
            if (isCannonAlligned)
                ShootIfPossible(enemy);


            // Find shortest path to enemy
            // This returns the next position to go
            Point relativePoint = NextTileToEnemy(enemy);
            Point nextPositionPoint = new Point(
                    (ControlledTank.Position.X / CollisionHandler.map.TileWidth + relativePoint.X) * CollisionHandler.map.TileWidth + CollisionHandler.map.TileWidth / 2,
                    (ControlledTank.Position.Y / CollisionHandler.map.TileHeight + relativePoint.Y) * CollisionHandler.map.TileHeight + CollisionHandler.map.TileHeight / 2
                );

            //NextPosition = nextPositionPoint;

            if (relativePoint.X == 0 && relativePoint.Y == 0)
            {
                ControlledTank.Flags.RotateBody = 0;
            }

            else
            {
                
                // Obtain the direction
                int direction = 0;
                while (direction < directions.Length)
                {
                    if (relativePoint.Equals(directions[direction]))
                        break;
                    direction++;
                }

                //double verify = angles[direction];
                double targetAngle = GetTargetAngle(nextPositionPoint, ControlledTank.CenterPosition);

                double normalized = ControlledTank.TankAngle < 0 ? ControlledTank.TankAngle + 360 : ControlledTank.TankAngle;

                double difference = normalized - targetAngle;
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
                    ControlledTank.Flags.RotateBody = -1;
                else
                    ControlledTank.Flags.RotateBody = 1;
                
            }

            // Move if it is about other space
            if (relativePoint.X == 0 && relativePoint.Y == 0)
            {
                // Same square, stay there
                ControlledTank.Flags.Move = 0;
            }
            else
            {
                // Must cover ground
                ControlledTank.Flags.Move = 1;
            }

        }

        private bool CannonFollowEnemy(Tank enemy)
        {
            // Get the angle between enemy and this
            double targetedAngle = GetTargetAngle(enemy.CenterPosition, ControlledTank.CenterPosition);
            double normalizedAngle = ControlledTank.CannonAngle;
            if (normalizedAngle < 0) normalizedAngle += 360;

            double difference = normalizedAngle - targetedAngle;
            double leftDistance, rightDistance;

            if(Math.Abs(difference) <= 5)
            {
                ControlledTank.Flags.RotateCannon = 0;
                return true;
            }

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
                ControlledTank.Flags.RotateCannon = -1;
            else
                ControlledTank.Flags.RotateCannon = 1;

            return false;
        }

        private void ShootIfPossible(Tank enemy)
        {
            // Check shoot timer
            if (timeSinceShoot < SHOOT_PAUSE_MILISECONDS)
                return;

            Point startPos = ControlledTank.ProjectilePosition();
            Point endPos = enemy.CenterPosition;

            double distanceBetween = Math.Sqrt((startPos.X - endPos.X)*(startPos.X - endPos.X) + (startPos.Y - endPos.Y) * (startPos.Y - endPos.Y));

            // Split path into divisions with the size of a Tile
            int tileSize = CollisionHandler.map.TileHeight;
            int radius = 5;
            
            for(double distance = 0; distance <= distanceBetween; distance += tileSize)
            {
                double u = distance / distanceBetween;

                // Parametrized line
                Point toBeEvaluated = new Point(
                    (int)(startPos.X + u*(endPos.X - startPos.X)) - radius,
                    (int)(startPos.Y + u*(endPos.Y - startPos.Y)) - radius
                    );

                // Check if collide with obstacle
                bool isCollision = CollisionHandler.CheckCollisionWithMap(new Rectangle(toBeEvaluated, new Size(2 * radius, 2 * radius)));

                // Not shoot if collision
                if (isCollision)
                {
                    return;
                }
            }

            // No collision detected. Shoot
            ControlledTank.Flags.Shoot = true;
            timeSinceShoot = 0;

        }

        private Point NextTileToEnemy(Tank enemy)
        {
            // Extract map information
            int[,] mapMatrix = CollisionHandler.map.Tiles;
            int height = mapMatrix.GetLength(0);
            int width = mapMatrix.GetLength(1);
            int tileHeight = CollisionHandler.map.TileHeight;
            int tileWidth = CollisionHandler.map.TileWidth;

            // Get the index of the enemy on the map
            // First component is Height, second is Width
            // The indexing is ajusted to the one that starts counting from 1
            Point enemyIndexes = new Point(enemy.CenterPosition.Y / tileHeight + 1, enemy.CenterPosition.X / tileWidth + 1);
            Point tankIndexes = new Point(ControlledTank.CenterPosition.Y / tileHeight + 1, ControlledTank.CenterPosition.X / tileWidth + 1);

            // Check if both are on the same tile, jump to conclusion
            if (enemyIndexes.Equals(tankIndexes))
            {
                return new Point(0, 0);
            }

            // Get the bordered matrix on which Lee algorithm will be performed
            // -1 = obstacle
            int[,] border = new int[height + 2, width + 2];
            // Ajust the borders of the matrix
            //
            // -1 -1 -1 -1 -1
            // -1          -1
            // -1          -1
            // -1 -1 -1 -1 -1

            for (int i=1; i <= height; i++)
                for (int j=1; j <= width; j++)
                    border[i, j] = mapMatrix[i - 1, j - 1] == 1 ? -1 : 0;

            for(int i=0; i <= height + 1; i++)
                border[i, 0] = border[i, width + 1] = -1;
            for(int j=0; j<=width + 1; j++)
                border[0, j] = border[height + 1, j] = -1;

            

            // Get the queue
            Queue<Point> toBeVisited = new Queue<Point>();

            // Push the starting point
            toBeVisited.Enqueue(tankIndexes);

            // Set the initial distance as 1
            border[tankIndexes.X, tankIndexes.Y] = 1;

            // While there are points to be visited
            while(toBeVisited.Count != 0)
            {
                // Get the first in the queue
                Point currentPoint = toBeVisited.Dequeue();

                // If it is the enemy, we found the path, we can stop
                if (currentPoint.Equals(enemyIndexes))
                    break;

                // Foreach neighbour, check if it can be visited
                foreach(Point delta in directions)
                {
                    // Get the neigbour by incrementing the position with delta
                    Point neighbour = new Point(currentPoint.X + delta.X, currentPoint.Y + delta.Y);

                    // If it can be visited
                    //      - it has border = 0 (it was'n visited before and it is not margin)
                    //      - there is no obstacle on the map
                    if(border[neighbour.X, neighbour.Y] == 0 && mapMatrix[neighbour.X - 1, neighbour.Y -1] != 1)
                    {
                        // Update the border
                        border[neighbour.X, neighbour.Y] = border[currentPoint.X, currentPoint.Y] + 1;

                        // Add the neighbour to the queue
                        toBeVisited.Enqueue(neighbour);
                    }
                }
            }

            // Obtain the list with the path
            Point lastPoint = enemyIndexes;
            Point current = enemyIndexes;
            //Stack<Point> path = new Stack<Point>();

            // Start from the end
            //path.Push(enemyIndexes);

            // Top element
            //Point current = path.Peek();

            // While not in the player place
            while (!current.Equals(tankIndexes))
            {
                // Get the anterior position by checking all neighbours
                int nextX = -10, nextY = -10;
                Point nextPoint;
                foreach (Point delta in directions)
                {
                    // Get the neigbour by incrementing the position with delta
                     nextPoint = new Point(current.X + delta.X, current.Y + delta.Y);

                    // The difference between 2 adiacent points in path is 1
                    if(border[nextPoint.X, nextPoint.Y] - border[current.X, current.Y] == -1)
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
                 nextPosition.Y - tankIndexes.Y,
                 nextPosition.X - tankIndexes.X
                );
            

            //return new Point(-1, 0);
        }

        private double GetTargetAngle(Point target, Point currentPosition)
        {
            Point lineVector = new Point(target.X - currentPosition.X, target.Y - currentPosition.Y);
            Point versor = new Point(1, 0);

            double dotProduct = lineVector.X * versor.X + lineVector.Y * versor.Y;
            double module = Math.Sqrt(lineVector.X * lineVector.X + lineVector.Y * lineVector.Y);

            // To consider when module is 0
            //...

            //...

            double targetedAngle = Math.Acos(dotProduct / module);

            // To degrees
            targetedAngle = ((180.0 / Math.PI) * targetedAngle) % 360;



            if (targetedAngle < 0) targetedAngle += 360;

            // Check position and flip angle if needed
            if (target.Y < currentPosition.Y)
                targetedAngle = 360 - targetedAngle;

            return targetedAngle;
        }
    }
}
