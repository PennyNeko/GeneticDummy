using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticDummy
{
    interface ISelection
    {
        int Selection(int[] weights, Random rand);
    }
}
