using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tancuri
{
    public class PlayerController : KeyboardController
    {
        protected Keys forwardKey;
        protected Keys backwardKey;
        protected Keys rotateLeftKey;
        protected Keys rotateRightKey;
        protected Keys cannonLeftKey;
        protected Keys cannonRightKey;
        protected Keys shootKey;

        public override void KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == cannonLeftKey)
                ControlledTank.Flags.RotateCannon = -1;

            if(e.KeyCode == cannonRightKey)
                ControlledTank.Flags.RotateCannon = 1;

            if(e.KeyCode == rotateLeftKey)
                ControlledTank.Flags.RotateBody = -1;

            if(e.KeyCode == rotateRightKey)
                ControlledTank.Flags.RotateBody = 1;

            if (e.KeyCode == forwardKey)
                ControlledTank.Flags.Move = 1;

            if (e.KeyCode == backwardKey)
                ControlledTank.Flags.Move = -1;

            if (e.KeyCode == shootKey)
                ControlledTank.Flags.Shoot = true;
        }

        public override void KeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == cannonLeftKey || e.KeyCode == cannonRightKey)
                ControlledTank.Flags.RotateCannon = 0;

            if (e.KeyCode == rotateLeftKey || e.KeyCode == rotateRightKey)
                ControlledTank.Flags.RotateBody = 0;

            if (e.KeyCode == forwardKey || e.KeyCode == backwardKey)
                ControlledTank.Flags.Move = 0;
        }

        public override void Update(){}
    }
}
