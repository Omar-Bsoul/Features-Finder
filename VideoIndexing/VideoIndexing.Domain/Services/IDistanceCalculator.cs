using System.Collections.Generic;

namespace VideoIndexing.Domain.Services {
    public interface IDistanceCalculator {
        double Calculate(IEnumerable<uint> ls1, IEnumerable<uint> ls2);
    }
}