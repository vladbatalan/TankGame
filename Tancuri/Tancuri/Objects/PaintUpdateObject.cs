using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tancuri
{
    public interface PaintUpdateObject
    {
        void Paint(Graphics g);
        void Update();

        Rectangle GetBody();
    }
}
