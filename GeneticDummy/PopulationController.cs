using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticDummy
{
    class PopulationController
    {
        Individual[] InitializePopulation(int populationSize, int numberOfBlocks, int availableColours, Random random)
        {
            Individual[] population = new Individual[populationSize];

            for (int i = 0; i < populationSize; i++)
            {
                int[] randomSequence = new int[numberOfBlocks];
                for (int j = 0; j < numberOfBlocks; j++)
                {
                    randomSequence[i] = random.Next(availableColours);
                }
                population[i] = new Individual(randomSequence);
            }
            return population;
        }

        void CalculateFitness(ConnectedBlocksGraph connectedBlocks, Individual individual, int positiveFitnessPoints, int negativeFitnessPoints)
        {
            for (int j = 0; j < connectedBlocks.Blocks.Count; j++)
            {
                for (int k = 0; k < connectedBlocks.Blocks[j].Length; k++)
                {
                    if (individual.Sequence[j] == (connectedBlocks.Blocks[j][k] - 1))
                    {
                        individual.Fitness -= negativeFitnessPoints;
                    }
                    else
                    {
                        individual.Fitness += positiveFitnessPoints;
                    }
                }
            }
        }

        
        Individual[,] ParentSelection(Population population, Random random)
        {
            RouletteWheelSelection rouletteWheelSelection = new RouletteWheelSelection();
            //Select two parents with roulette wheel selection
            Individual[,] selectedParents = new Individual[2, population.Individuals[0].Sequence.Length];
            int firstParentPosition = rouletteWheelSelection.Selection(population.GetFitness(), random);
            int secondParentPosition = rouletteWheelSelection.Selection(population.GetFitness(), random);
            //Ensure that the two parents are different
            while (firstParentPosition == secondParentPosition)
            {
                secondParentPosition = rouletteWheelSelection.Selection(population.GetFitness(), random);
            }
            //Add parents in list to return
            for (int i = 0; i < population.Individuals[0].Sequence.Length; i++)
            {
                selectedParents[0, i] = population.Individuals[firstParentPosition].Sequence[i];
                selectedParents[1, i] = population.Individuals[secondParentPosition].Sequence[i];
            }

            return selectedParents;
        }
    }
}
