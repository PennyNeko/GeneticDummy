using System;
using System.Linq;

namespace GeneticDummy
{
    class PopulationController
    {
        /**
         *The method that combines the methods below to actually compute the result of the genetic algorithm.
         * It initializes the population at first. Then, it calculates the fitness, and checks if the maximum fitness has been reached.
         * Continues until the max fitness has been reached, by creating a new population with recombination and mutation.
         */
        public void StartGeneticAlgorithmProcess(GeneticOptions geneticOptions, string graphPath, Random random)
        {
            int iterations = 0;
            ConnectedBlocksGraph connectedBlocks = new ConnectedBlocksGraph(graphPath);
            Population population = InitializePopulation(geneticOptions, connectedBlocks.Blocks.Count, random);
            foreach (var i in population.Individuals)
                i.CalculateFitness(connectedBlocks, geneticOptions);
            while (!CanEnd(connectedBlocks, population, geneticOptions))
            {
                Population newPopulation = RefreshPopulation(population, connectedBlocks, geneticOptions, random);
                newPopulation = ApplyMutation(newPopulation, random, geneticOptions);
                population = newPopulation;
                iterations++;
            }
            int maxFitness = GetMaxFitness(connectedBlocks, geneticOptions);
            ResultPrinting(iterations, population, maxFitness);
        }
        /**
         * Prints on the console the result of the genetic algothim.
         */
        private void ResultPrinting(int iterations, Population population, int maxFitness)
        {
            Console.WriteLine($"Max fitness of {maxFitness} reached.");
            int[] fitness = population.GetFitness();
            int maxFitnessPosition = 0;
            for (int i = 0; i < fitness.Length; i++)
            {
                if (fitness[i] == maxFitness)
                    maxFitnessPosition = i;
            }
            for (int i=0; i< population.Individuals[maxFitnessPosition].Sequence.Length; i++)
            {
                Console.WriteLine($"Genome {i+1} value: {population.Individuals[maxFitnessPosition].Sequence[i]}");
            }
            Console.WriteLine($"Iterations needed: {iterations}");
        }

        /**
        * Initializes the population randomly for the first iteration of the algorithm.
        */
        Population InitializePopulation(GeneticOptions geneticOptions, int numberOfBlocks, Random random)
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

        /**
         *A method that selects two different parents to be used for recombination.
         * @return A 2D array containing each parent in each row.
         */
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
        /**
         * Calculates the maximum available fitness by multiplying the amount of block pairs with the amount of reward points.
         */
        int GetMaxFitness(ConnectedBlocksGraph connectedBlocks, GeneticOptions geneticOptions)
        {
            int maxFitness = 0;
            foreach (var a in connectedBlocks.Blocks)
            {
                foreach (var i in a)
                {
                    maxFitness++;
                }
            }
            return maxFitness*geneticOptions.PositiveFitnessPoints;
        }
        /**
         * Creates a new population by recombining the parents to create children. Selects the parents to stay in the new population with roulette wheel selection.
         * @see ParentRecombination()
         */
        Population RefreshPopulation(Population population, ConnectedBlocksGraph connectedBlocks, GeneticOptions geneticOptions, Random random)
        {
            RouletteWheelSelection rouletteWheelSelection = new RouletteWheelSelection();
            int childrenAmount = (int)(geneticOptions.PopulationSize * geneticOptions.PopulationRefreshing);
            Individual[] children = new Individual[geneticOptions.PopulationSize];
            for (int i = 0; i < childrenAmount; i++)
            {
                int[,] parents = ParentSelection(population, random);
                children[i] = ParentRecombination(new Individual(Util.GetRow(parents, 0)), new Individual(Util.GetRow(parents, 1)), random);
                children[i].CalculateFitness(connectedBlocks, geneticOptions);
            }
            for (int i = 0; i < geneticOptions.PopulationSize - childrenAmount; i++)
            {
                children[i + childrenAmount] = population.Individuals[rouletteWheelSelection.Selection(population.GetFitness(), random)];
            }
            return new Population(children);
        }
        /**
         *Checks if the maximum fitness value has been reached, by checking if the population has an individual with the maximum amount of fitness. 
         */
        bool CanEnd(ConnectedBlocksGraph connectedBlocks, Population population, GeneticOptions geneticOptions)
        {
            return GetMaxFitness(connectedBlocks, geneticOptions) == population.GetFitness().Max();
        }

        /**
         * Applies mutation to the whole population based on the mutation chance.
         * @return Returns a population object with the mutated individuals.
         */
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
        /**
         * Takes to parents, selects a random split position. Creates a child containing the genomes of the first parent up to the splitting point,
         * and fills the rest with the second parent.
         * param parentA The first parent of type Individual.
         * param parentA The second parent.
         * param random The random number generator.
         */
        Individual ParentRecombination(Individual parentA, Individual parentB, Random random)
        {
            int splitPosition = random.Next(parentA.Sequence.Length);
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
