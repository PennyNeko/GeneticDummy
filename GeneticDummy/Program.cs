using System;

namespace GeneticDummy
{
    class Program
    {
        const int POPULATION_SIZE = 100;
        const double MUTATION_CHANCE = 0.1;
        const int AVAILABLE_COLOURS = 4;
        const int POSITIVE_FITNESS_POINTS = 1;
        const int NEGATIVE_FITNESS_POINTS = -1;
        const double POPULATION_REFRESHING = 0.8;

        static void Main(string[] args)
        {
            string blocksPath = args[0];
            PopulationController populationController = new PopulationController();
            GeneticOptions geneticOptions = new GeneticOptions(POPULATION_SIZE, AVAILABLE_COLOURS, MUTATION_CHANCE, POSITIVE_FITNESS_POINTS, NEGATIVE_FITNESS_POINTS, POPULATION_REFRESHING);
            populationController.StartGeneticAlgorithmProcess(geneticOptions, blocksPath, new Random());

        }
    }
}
