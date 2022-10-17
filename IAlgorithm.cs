using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    public interface IAlgorithm
    {
        int next_step();
        int score(PlayingField field);
    }
}
