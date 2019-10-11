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

        public void Mutate(Random random, GeneticOptions geneticOptions)
        {
            int randomPosition = random.Next(Sequence.Length);
            int randomColour = random.Next(geneticOptions.AvailableColours);
            //Ensure that the mutation results to a different colour
            while (randomColour == Sequence[randomPosition])
            {
                randomColour = random.Next(geneticOptions.AvailableColours);
            }
            Sequence[randomPosition] = randomColour;
        }

        public void CalculateFitness(ConnectedBlocksGraph connectedBlocks, GeneticOptions geneticOptions)
        {
            for (int j = 0; j < connectedBlocks.Blocks.Count; j++)
            {
                for (int k = 0; k < connectedBlocks.Blocks[j].Length; k++)
                {
                    if (Sequence[j] == (connectedBlocks.Blocks[j][k] - 1))
                    {
                        Fitness += geneticOptions.NegativeFitnessPoints;
                    }
                    else
                    {
                        Fitness += geneticOptions.PositiveFitnessPoints;
                    }
                }
            }
        }

    }
}
