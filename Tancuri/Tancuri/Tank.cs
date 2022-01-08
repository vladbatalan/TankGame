using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tancuri
{
    public class Tank : PaintUpdateObject
    {
        public const int MAX_AMMUNATION = 10;
        public const int TANK_SIZE = 30;
        public const int CANNON_WIDTH = 6;
        public const int CANNON_LENGTH = 40;
        public const float SPEED = 5;
        public const float TANK_ROTATION_SPEED = 5;
        public const float CANNON_ROTATION_SPEED = 5;

        private float _cannonAngle;
        private float _tankAngle;
        private int _ammunition;

        public MovingFlags Flags { set; get; }


        /// <summary>
        /// The health of the Tank
        /// </summary>
        public int Health { set; get; }
        public int Ammunition { set => _ammunition = Math.Max(0, Math.Min(value, MAX_AMMUNATION)); get => _ammunition; }


        /// <summary>
        /// The position of the tank and the cannon
        /// </summary>
        public Point Position { set; get; }
        public Point CannonPosition => new Point(Position.X + TANK_SIZE / 2, Position.Y + TANK_SIZE / 2 - CANNON_WIDTH / 2);
        public Point ProjectilePosition() {
            
            // Get the position of the cannon
            double xPos = CannonPosition.X;
            double yPos = CannonPosition.Y;


            // Add the length of the cannon
            xPos += CANNON_LENGTH * Math.Cos((Math.PI / 180) * CannonAngle);
            yPos += CANNON_LENGTH * Math.Sin((Math.PI / 180) * CannonAngle);


            // Add the width of the cannon/2
            xPos += CANNON_WIDTH * Math.Cos((Math.PI / 180) * (CannonAngle - 90));
            yPos += CANNON_WIDTH * Math.Sin((Math.PI / 180) * (CannonAngle - 90));

            return new Point((int)xPos, (int)yPos);
        }
        public Point CenterPosition { get => new Point(Position.X + TANK_SIZE / 2, Position.Y + TANK_SIZE / 2); }
        public Brush TankBrush { get; set; }


        /// <summary>
        /// The rotation angle
        /// </summary>
        public float TankAngle { set => _tankAngle = value % 360; get => _tankAngle; }
        public float CannonAngle { set => _cannonAngle = value % 360; get => _cannonAngle; }


        public Tank()
        {
            Position = new Point(0, 0);
            CannonAngle = 0;
            TankAngle = 0;
            Health = 100;
            Ammunition = MAX_AMMUNATION;
            Flags = new MovingFlags();
            
        }
        public Tank(Point position, float rotationAngle = 0, float cannonRotationAngle = 0)
        {
            Position = position;
            CannonAngle = cannonRotationAngle;
            TankAngle = rotationAngle;
            Health = 100;
            Ammunition = MAX_AMMUNATION;
            Flags = new MovingFlags();
        }

        public void Paint(Graphics g)
        {
            Brush tankFill = TankBrush;
            Brush cannonFill = Brushes.BurlyWood;

            Rectangle tankBody = new Rectangle(new Point(-TANK_SIZE/2, -TANK_SIZE/2), new Size(TANK_SIZE, TANK_SIZE));
            Rectangle cannonBody = new Rectangle(new Point(0, -CANNON_WIDTH/2), new Size(CANNON_LENGTH, CANNON_WIDTH));
            Rectangle tankFront = new Rectangle(new Point(TANK_SIZE/2, TANK_SIZE / 2 - 25), new Size(10, 20));

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw Body 
            g.TranslateTransform(Position.X + TANK_SIZE / 2, Position.Y + TANK_SIZE / 2);
            g.RotateTransform(TankAngle);
            g.FillRectangle(tankFill, tankBody);
            g.FillRectangle(tankFill, tankFront);

           /* g.DrawLine(Pens.Black, tankBody.Location, new Point(TANK_SIZE / 2, TANK_SIZE / 2));
            g.DrawLine(Pens.Black, new Point(TANK_SIZE/2, -TANK_SIZE/2), new Point(-TANK_SIZE / 2, TANK_SIZE / 2));*/


           /* g.DrawLine(Pens.Green, new Point(tankBody.Location.X + TANK_SIZE/2, tankBody.Location.Y + TANK_SIZE/2), 
                new Point(tankBody.Location.X + TANK_SIZE / 2 + 60, tankBody.Location.Y + TANK_SIZE / 2));*/
            g.RotateTransform(-TankAngle);

            // Draw cannon
            g.RotateTransform(CannonAngle);
            g.FillRectangle(cannonFill, cannonBody);
            // g.RotateTransform(-CannonRotationAngle);

            // Bring scene back
            g.ResetTransform();

        }

        public void Update()
        {

            // Execute shoot
            if (Flags.Shoot)
            {
                Flags.Shoot = false;

                // If there is enough ammunition
                if (Ammunition != 0)
                {
                    // Create object for objectHandler
                    Projectile projectile = new Projectile(ProjectilePosition(), CannonAngle);

                    // Add to Handler
                    ObjectHandler.AllObjects.Enqueue(projectile);

                    Ammunition--;
                }
            }

            // Execute move
            if (Flags.Move != 0)
            {
                int dx = (int)(SPEED * Math.Cos((Math.PI / 180) * TankAngle));
                int dy = (int)(SPEED * Math.Sin((Math.PI / 180) * TankAngle));

                Point newPosition = new Point(Position.X + Flags.Move * dx, Position.Y + Flags.Move * dy);
                bool isCollision = CollisionHandler.CheckCollisionWithMap(new Rectangle(newPosition, new Size(TANK_SIZE, TANK_SIZE)));

                // Check colision
                if (isCollision == false)
                {
                    Position = newPosition;
                }
            }

            // Update angles
            if (Flags.RotateBody != 0)
            {
                TankAngle = TankAngle + Flags.RotateBody * TANK_ROTATION_SPEED;
            }
            if (Flags.RotateCannon != 0)
            {
                CannonAngle = CannonAngle + Flags.RotateCannon * CANNON_ROTATION_SPEED;
            }
        }

        public Rectangle GetBody()
        {
            return new Rectangle(Position, new Size(TANK_SIZE, TANK_SIZE));
        }
    }
}
