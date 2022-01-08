using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tancuri
{
    public class Player2Controller : PlayerController
    {
        public Player2Controller(Tank tank)
        {
            ControlledTank = tank;

            forwardKey = Keys.W;
            backwardKey = Keys.S;

            rotateLeftKey = Keys.A;
            rotateRightKey = Keys.D;

            cannonLeftKey = Keys.I;
            cannonRightKey = Keys.P;

            shootKey = Keys.O;
        }

    }
}
