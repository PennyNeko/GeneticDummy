using System;
using System.Collections.Generic;
using System.IO;

namespace GeneticDummy
{
    public class ConnectedBlocksGraph
    {
        public ConnectedBlocksGraph(string path)
        {
            Blocks = GenerateConnectedBlocks(path);
        }

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

        public List<int[]> Blocks { set; get; }
        
    }
}
