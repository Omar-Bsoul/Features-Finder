using IoCManager.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoIndexing.Domain.Services {
    public interface IJaccardCalculator : IDistanceCalculator, ITransientDependency {
        double Calculate(ISet<uint> hs1, ISet<uint> hs2);
    }
}
