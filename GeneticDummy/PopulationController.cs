using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticDummy
{
    class PopulationController
    {
        public void StartGeneticAlgorithmProcess(GeneticOptions geneticOptions, string graphPath, Random random)
        {
            int iterations = 0;
            ConnectedBlocksGraph connectedBlocks = new ConnectedBlocksGraph(graphPath);
            Population population = InitializePopulation(geneticOptions, connectedBlocks.Blocks.Count, random);
            foreach (var i in population.Individuals)
                i.CalculateFitness(connectedBlocks, geneticOptions);
            while (!CanEnd(connectedBlocks, population))
            {
                Population newPopulation = RefreshPopulation(population,connectedBlocks,geneticOptions,random);
                newPopulation = ApplyMutation(newPopulation, random, geneticOptions);
                population = newPopulation;
                iterations++;
            }
            Console.WriteLine(GetMaxFitness(connectedBlocks));
            foreach(var f in population.GetFitness())
                Console.WriteLine(f);
            Console.WriteLine(population.GetFitness().Max());
            Console.WriteLine(iterations);
        }
        Population InitializePopulation(GeneticOptions geneticOptions, int numberOfBlocks,Random random)
        {
            Individual[] individuals = new Individual[geneticOptions.PopulationSize];

            for (int i = 0; i < geneticOptions.PopulationSize; i++)
            {
                int[] randomSequence = new int[numberOfBlocks];
                for (int j = 0; j < numberOfBlocks; j++)
                {
                    randomSequence[j] = random.Next(geneticOptions.AvailableColours);
                }
                individuals[i] = new Individual(randomSequence);
            }
            return new Population(individuals);
        }

        
        int[,] ParentSelection(Population population, Random random)
        {
            RouletteWheelSelection rouletteWheelSelection = new RouletteWheelSelection();
            //Select two parents with roulette wheel selection
            int[,] selectedParents = new int[2, population.Individuals[0].Sequence.Length];
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
        int GetMaxFitness(ConnectedBlocksGraph connectedBlocks)
        {
            int maxFitness = 0;
            foreach (var a in connectedBlocks.Blocks)
            {
                foreach (var i in a)
                {
                    maxFitness++;
                }
            }
            return maxFitness;
        }

        Population RefreshPopulation(Population population, ConnectedBlocksGraph connectedBlocks, GeneticOptions geneticOptions, Random random)
        {
            RouletteWheelSelection rouletteWheelSelection = new RouletteWheelSelection();
            int childrenAmount =  (int)(geneticOptions.PopulationSize * geneticOptions.PopulationRefreshing);
            Individual[] children = new Individual[geneticOptions.PopulationSize];
            for(int i = 0; i< childrenAmount; i++)
            {
                int[,] parents = ParentSelection(population, random);
                children[i] = ParentRecombination(new Individual(Util.GetRow(parents, 0)), new Individual(Util.GetRow(parents, 1)), random);
                children[i].CalculateFitness(connectedBlocks, geneticOptions);
            }
            for (int i = 0; i < geneticOptions.PopulationSize - childrenAmount ; i++)
            {
                children[i + childrenAmount] = population.Individuals[rouletteWheelSelection.Selection(population.GetFitness(), random)];
            }
            return new Population(children);
        }

        bool CanEnd(ConnectedBlocksGraph connectedBlocks, Population population)
        {
            return GetMaxFitness(connectedBlocks) == population.GetFitness().Max();
        }


        Population ApplyMutation(Population population, Random random, GeneticOptions geneticOptions)
        {
            for (int i = 0; i < population.Individuals.Length; i++)
            {
                if (random.NextDouble() < geneticOptions.MutationChance)
                {
                    population.Individuals[i].Mutate(random, geneticOptions);
                }
            }
            return population;
        }

        Individual ParentRecombination(Individual parentA, Individual parentB, Random rand)
        {
            int splitPosition = rand.Next(parentA.Sequence.Length);
            int[] child = new int[parentA.Sequence.Length];
            for (int i = 0; i < parentA.Sequence.Length; i++)
            {
                if (i <= splitPosition)
                {
                    child[i] = parentA.Sequence[i];
                }
                else
                {
                    child[i] = parentB.Sequence[i];
                }
            }
            return new Individual(child);
        }
    }
}
