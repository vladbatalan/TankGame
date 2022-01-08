using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tancuri
{
    public abstract class KeyboardController
    {
        protected Tank _tank;

        public Tank ControlledTank { set => _tank = value; get => _tank; }

        public abstract void KeyDown(KeyEventArgs e);
        public abstract void KeyUp(KeyEventArgs e);
        public abstract void Update();
    }
}
