using System;
using System.Collections.Generic;
using System.IO;

namespace GeneticDummy
{
    /**
     * A class containing all the information related to the graph to be painted.
     */
    public class ConnectedBlocksGraph
    {
        /**
         * @params path The path to the file of the .txt that contains which block touches which.
         */
        public ConnectedBlocksGraph(string path)
        {
            Blocks = GenerateConnectedBlocks(path);
        }

        /**
         * Creates a list of arrays, where every array represents which number of block, every block in a row touches.
         */
        List<int[]> GenerateConnectedBlocks(string path)
        {
            List<int[]> connectedBlocks = new List<int[]>();
            foreach (string line in File.ReadLines(path))
            {
                string[] blocks = line.Split(',');
                connectedBlocks.Add(Array.ConvertAll(blocks, int.Parse));
            }
            return connectedBlocks;
        }
        /// Sets and gets which block touches which.
        public List<int[]> Blocks { set; get; }

    }
}
