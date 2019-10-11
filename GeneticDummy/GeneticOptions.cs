using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticDummy
{
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

        public int PopulationSize { set; get; }
        public int AvailableColours { set; get; }
        public double MutationChance { set; get; }
        public int PositiveFitnessPoints { set; get; }
        public int NegativeFitnessPoints { set; get; }
        public double PopulationRefreshing { set; get; }
    }
}
