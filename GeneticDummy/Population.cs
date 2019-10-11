using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticDummy
{
    class Population
    {
        public Population(Individual[] individuals)
        {
            Individuals = individuals;
        }

        public Individual[] Individuals { set; get; } 
        public int[] GetFitness()
        {
            int[] fitness = new int[Individuals.Length];
            for (int i = 0; i <Individuals.Length; i++)
            {
                fitness[i] = Individuals[i].Fitness;
            }
            return fitness;
        }

    }
}
