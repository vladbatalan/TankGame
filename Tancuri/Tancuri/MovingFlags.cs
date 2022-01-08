using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tancuri
{
    public class MovingFlags
    {
        /// <summary>
        /// 1 for left/forward, 0 for no move -1 for right/backward
        /// </summary>
        public int Move { set; get; }
        public int RotateBody { set; get; } 
        public int RotateCannon { set; get; }
        public bool Shoot { set; get; }

        public MovingFlags()
        {
            Move = 0;
            RotateBody = 0;
            RotateCannon = 0;
            Shoot = false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Move = " + Move);
            sb.AppendLine("RotateBody = " + RotateBody);
            sb.AppendLine("RotateCannon = " + RotateCannon);
            sb.AppendLine("Shoot = " + Shoot.ToString());

            return sb.ToString();
        }
    };
}
