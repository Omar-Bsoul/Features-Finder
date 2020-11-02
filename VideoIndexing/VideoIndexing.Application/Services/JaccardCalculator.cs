using System.Collections.Generic;
using System.Linq;
using VideoIndexing.Domain.Services;

namespace VideoIndexing.Application.Services {
    public class JaccardCalculator : IJaccardCalculator {
        // Calculates the similarity of two lists of min hash values. Approximately Numerically equivilant to Jaccard Similarity
        public double Calculate(IEnumerable<uint> ls1, IEnumerable<uint> ls2) {
            var hs1 = new HashSet<uint>(ls1);
            var hs2 = new HashSet<uint>(ls2);

            return Calculate(hs1, hs2);
        }

        public double Calculate(ISet<uint> hs1, ISet<uint> hs2) {
            return (hs1.Intersect(hs2).Count() / (double)hs1.Union(hs2).Count());
        }
    }
}