namespace GeneticDummy
{
    /// Class containing an array of Individuals and its properties.
    class Population
    {
        public Population(Individual[] individuals)
        {
            Individuals = individuals;
        }
        ///An array of Individual objects 
        public Individual[] Individuals { set; get; }
        /**
         * Calculates the fitness of the whole population
         * @return an array of the size of the population containing the fitness values of each individual.
         */
        public int[] GetFitness()
        {
            int[] fitness = new int[Individuals.Length];
            for (int i = 0; i < Individuals.Length; i++)
            {
                fitness[i] = Individuals[i].Fitness;
            }
            return fitness;
        }

    }
}
