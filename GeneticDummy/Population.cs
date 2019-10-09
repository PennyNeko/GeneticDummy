using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticDummy
{
    class Population
    {
        public Population()
        {

        }

        Genome[] Genomes { set; get; } 

        void InitializePopulation(int populationSize, int numberOfBlocks, Random random)
        {
            Genomes = new Genome[populationSize, numberOfBlocks];

            for (int i = 0; i < populationSize; i++)
            {
                for (int j = 0; j < numberOfBlocks; j++)
                {
                    Genomes[i, j] = random.Next(4);
                }
            }
        }
    }
}
