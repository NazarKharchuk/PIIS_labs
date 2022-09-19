using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class AStarNode
    {
        public Cell coordinates { get; set; }
        public Cell parent { get; set; }
        public int g_value { get; set; }
        public int h_value { get; set; }
        public int f_value { get; set; }

        public AStarNode()
        {
            coordinates = null;
            parent = null;
            g_value = h_value = f_value = 0;
        }

        public AStarNode(Cell _coordinates, Cell _parent, int g, int h)
        {
            coordinates = _coordinates;
            parent = _parent;
            g_value = g;
            h_value = h;
            f_value = g + h;
        }
    }
}
