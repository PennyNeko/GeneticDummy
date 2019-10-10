using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticDummy
{
    public class RouletteWheelSelection : ISelection
    {
        public int Selection(int[] weights, Random rand)
        {
            int weightSum = weights.Sum();
            int randomInt = rand.Next(weightSum);
            for (int i = 0; i < weights.Length; i++)
            {
                if (randomInt < weights[i])
                {
                    return i;
                }
                randomInt -= weights[i];
            }
            throw new Exception("End of roulette wheel selection was reached, could not select an int.");
        }
    }
}
