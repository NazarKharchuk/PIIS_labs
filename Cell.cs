using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class Cell
    {
        public int x { get; set; }
        public int y { get; set; }

        public Cell()
        {
            x = 0;
            y = 0;
        }

        public Cell(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
