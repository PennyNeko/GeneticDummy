namespace GeneticDummy
{
    
    ///A class containing information regarding the initial parameters of the genetic algorithm, such as the population size, the mutation chance etc.
    class GeneticOptions
    {
        
        public GeneticOptions(int populationSize, int availableColours, double mutationChance, int positiveFitnessPoints, int negativeFitnessPoints, double populationRefreshing)
        {
            PopulationSize = populationSize;
            AvailableColours = availableColours;
            MutationChance = mutationChance;
            PositiveFitnessPoints = positiveFitnessPoints;
            NegativeFitnessPoints = negativeFitnessPoints;
            PopulationRefreshing = populationRefreshing;
        }
        /// Sets and gets the size of every population.
        public int PopulationSize { set; get; }
        /// The number of available colours to colour the graph.
        public int AvailableColours { set; get; }
        /// The chance to mutate an individual of the population.
        public double MutationChance { set; get; }
        /// The reward value that gets added to the fitness, with range from 0 to 1.
        public int PositiveFitnessPoints { set; get; }
        /// The punishment value that gets added to the fitness.
        public int NegativeFitnessPoints { set; get; }
        /// The percentage of the population that will be replaced with children, instead of keeping the parents, with range from 0 to 1.
        public double PopulationRefreshing { set; get; }
    }
}
