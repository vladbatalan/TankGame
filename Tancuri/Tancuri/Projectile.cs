using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tancuri
{
    public class Projectile : PaintUpdateObject
    {
        const float SPEED = 10;
        const int RADIUS = 5;
        public const int DAMAGE = 10;

        private float _angle;

        public float Angle { set => _angle = value % 360; get => _angle; }
        public Point Position { set; get; }

        public Projectile(Point position, float angle)
        {
            Position = position;
            Angle = angle;
        }

        public void Paint(Graphics g)
        {
            Brush projectileFill = Brushes.Gainsboro;
            g.FillEllipse(projectileFill, Position.X - RADIUS, Position.Y + RADIUS, 2 * RADIUS, 2 * RADIUS); 
        }


        public void Update()
        {
            int dx = (int)(SPEED * Math.Cos((Math.PI / 180) * Angle));
            int dy = (int)(SPEED * Math.Sin((Math.PI / 180) * Angle));

            Point newPosition = new Point(Position.X + dx, Position.Y + dy);
            Point startLocation = new Point(Position.X + dx - RADIUS, Position.Y + dy - RADIUS);

            // Check collision with the map
            bool isCollision = CollisionHandler.CheckCollisionWithMap(new Rectangle(startLocation, new Size(2*RADIUS, 2*RADIUS)));
            if (isCollision)
            {
                // Destroy object
                ObjectHandler.DestroyObject(this);
                return;
            }

            // Check if it hits other objects
            PaintUpdateObject hitObject = CollisionHandler.GetCollisionObject(this);

            if(hitObject != null)
            {
                // If it is a tank, it s life will decrease
                if(hitObject is Tank)
                {
                    Tank tank = (Tank)hitObject;

                    tank.Health -= DAMAGE;

                    ObjectHandler.DestroyObject(this);
                    return;
                }
            }

            Position = new Point(Position.X + dx, Position.Y + dy);
        }

        public Rectangle GetBody()
        {
            Point startLocation = new Point(Position.X - RADIUS, Position.Y - RADIUS);
            return new Rectangle(startLocation, new Size(2 * RADIUS, 2 * RADIUS));
        }
    }
}
