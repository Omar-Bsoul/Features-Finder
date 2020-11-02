using System;
using System.Collections.Generic;
using System.Linq;
using VideoIndexing.Domain.Services;

namespace VideoIndexing.Application.Services {
    public class MinHashCalculator : IMinHashCalculator {
        private readonly IJaccardCalculator jaccardCalculator;

        // Public access to hash functions
        public List<Hash> HashFunctions { get; private set; }

        public delegate uint Hash(int toHash);

        // Constructor passed universe size and number of hash functions
        public MinHashCalculator(IJaccardCalculator jaccardCalculator) {
            this.jaccardCalculator = jaccardCalculator;
        }

        private void InitMinHashFunctions(int universeCount, int numHashFunctions) {
            int universeSize = CalcUniverseSize(universeCount);
            HashFunctions = new List<Hash>(numHashFunctions);

            //int seed = Math.Max(universeCount, numHashFunctions) % Math.Min(universeCount, numHashFunctions);

            GenerateHashFunctions(universeSize/*, seed*/);
        }

        // Generates the Universal Random Hash functions
        // http://en.wikipedia.org/wiki/Universal_hashing
        private void GenerateHashFunctions(int universeSize, int seed = 10) {
            // will get the same hash functions each time since the same random number seed is used
            Random random = new Random(seed);
            for (int i = 0; i < HashFunctions.Count; i++) {
                uint a = 0;
                // parameter a is an odd positive
                while (a % 2 != 0 || a <= 0)
                    a = (uint)random.Next();
                uint b = 0;
                int maxb = 1 << universeSize;

                // parameter b must be greater than zero and less than universe size
                b = (uint)random.Next(maxb);

                HashFunctions.Add(x => QHash(x, a, b, universeSize));
            }
        }

        // Returns the number of bits needed to store the universe
        private int CalcUniverseSize(int universeSize) {
            return (int)Math.Ceiling(Math.Log(universeSize, 2));
        }

        // Universal hash function with two parameters a and b, and universe size in bits
        private static uint QHash(int x, uint a, uint b, int u) {
            return (a * (uint)x + b) >> (32 - u);
        }

        // Returns the list of min hashes for the given set of word Ids
        public IEnumerable<uint> Calculate(int universeCount, int numHashFunctions, IEnumerable<int> wordIds) {
            InitMinHashFunctions(universeCount, numHashFunctions);
            List<uint> minHashes = new List<uint>(HashFunctions.Count);

            for (int h = 0; h < HashFunctions.Count; h++) {
                minHashes.Add(int.MaxValue);
            }

            foreach (int id in wordIds) {
                for (int h = 0; h < HashFunctions.Count; h++) {
                    uint hash = HashFunctions[h](id);
                    minHashes[h] = Math.Min(minHashes[h], hash);
                    if (minHashes[h] == 0)
                        minHashes[h] = int.MaxValue;
                }
            }

            return minHashes.ToList();
        }
    }
}
