using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tancuri
{
    public class Player1Controller : PlayerController
    {
        public Player1Controller(Tank tank)
        {
            ControlledTank = tank;

            forwardKey = Keys.Up;
            backwardKey = Keys.Down;

            rotateLeftKey = Keys.Left;
            rotateRightKey = Keys.Right;

            cannonLeftKey = Keys.Z;
            cannonRightKey = Keys.C;

            shootKey = Keys.X;
        }   
    }
}
