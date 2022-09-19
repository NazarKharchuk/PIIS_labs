using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class Cell
    {
        public int col { get; set; }
        public int row { get; set; }

        public Cell()
        {
            col = 0;
            row = 0;
        }

        public Cell(int _col, int _row)
        {
            col = _col;
            row = _row;
        }
    }
}
