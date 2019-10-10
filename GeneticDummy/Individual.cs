using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticDummy
{
    class Individual
    {
        public Individual(int[] sequence)
        {
            Sequence = sequence;
        }

        public int Fitness { get; set; }
        public int[] Sequence { set; get; }

        void Mutate(Random random, int availableColours)
        {
            int randomPosition = random.Next(Sequence.Length);
            int randomColour = random.Next(availableColours);
            //Ensure that the mutation results to a different colour
            while (randomColour == Sequence[randomPosition])
            {
                randomColour = random.Next(availableColours);
            }
            Sequence[randomPosition] = randomColour;
        }

    }
}
