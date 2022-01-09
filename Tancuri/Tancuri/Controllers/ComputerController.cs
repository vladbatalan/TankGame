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

            if (relativePoint.X == 0 && relativePoint.Y == 0)
            {
                ControlledTank.Flags.RotateBody = 0;
            }
            else
            {
                
                // Obtain the direction
                int direction = 0;
                while (direction < StrategyUtil.directions.Length)
                {
                    if (relativePoint.Equals(StrategyUtil.directions[direction]))
                        break;
                    direction++;
                }

                //double verify = angles[direction];
                double targetAngle = StrategyUtil.EvaluateAngleBetweenPoints(nextPositionPoint, ControlledTank.CenterPosition);

                double normalized = ControlledTank.TankAngle < 0 ? ControlledTank.TankAngle + 360 : ControlledTank.TankAngle;

                ControlledTank.Flags.RotateBody = StrategyUtil.AjustRotation(targetAngle, normalized);
                
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
            double targetedAngle = StrategyUtil.EvaluateAngleBetweenPoints(enemy.CenterPosition, ControlledTank.CenterPosition);
            double normalizedAngle = ControlledTank.CannonAngle;
            if (normalizedAngle < 0) normalizedAngle += 360;

            double difference = normalizedAngle - targetedAngle;

            if(Math.Abs(difference) <= 5)
            {
                ControlledTank.Flags.RotateCannon = 0;
                return true;
            }
            ControlledTank.Flags.RotateCannon = StrategyUtil.AjustRotation(targetedAngle, normalizedAngle);

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
            int[,] border = StrategyUtil.LeeAlgoritm(mapMatrix, tankIndexes, enemyIndexes);

            return StrategyUtil.FindDirectionToGo(border, tankIndexes, enemyIndexes);
        }

    }
}
