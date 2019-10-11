using System;

namespace GeneticDummy
{
    /**
     * Class of an Individual of a population.
     */
    class Individual
    {
        public Individual(int[] sequence)
        {
            Sequence = sequence;
        }
        ///The fitness of each individual.
        public int Fitness { get; set; }
        /**
         * An individual's genome sequence. In this case the sequence is a number from 0 to AVAILABLE_COLOURS-1, each number corresponding to a different colour. 
         */
        public int[] Sequence { set; get; }
        /**
         * A class that mutates a genome's colour to a different colour randomly. 
         */
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
        ///A method that calculates the fitness of the individual based on the reward and punishment values.
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
