using System;

namespace GeneticDummy
{
    ///Interface to implement selection algorithms like roulette wheel selection, tournament selection etc.
    interface ISelection
    {
        int Selection(int[] weights, Random rand);
    }
}
