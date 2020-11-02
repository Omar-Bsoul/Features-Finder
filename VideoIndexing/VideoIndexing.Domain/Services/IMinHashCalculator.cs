using IoCManager.Dependency;
using System.Collections.Generic;

namespace VideoIndexing.Domain.Services {
    public interface IMinHashCalculator : ITransientDependency {
        IEnumerable<uint> Calculate(int universeCount, int numHashFunctions, IEnumerable<int> wordIds);
    }
}