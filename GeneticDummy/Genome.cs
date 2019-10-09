using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticDummy
{
    class Genome
    {
        public Genome(int[] sequence)
        {
            Sequence = sequence;
        }

        Genome Mutate(Genome genome, Random random, int availableColours)
        {
            int randomPosition = random.Next(genome.Sequence.Length);
            int randomColour = random.Next(availableColours);
            //Ensure that the mutation results to a different colour
            while (randomColour == genome.Sequence[randomPosition])
            {
                randomColour = random.Next(availableColours);
            }
            genome.Sequence[randomPosition] = randomColour;
            return genome;
        }

        int[] Sequence { set; get; }
    }
}
