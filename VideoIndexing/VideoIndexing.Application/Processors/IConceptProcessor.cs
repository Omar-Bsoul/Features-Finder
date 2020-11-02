using IoCManager.Dependency;
using System.Collections.Generic;
using System.IO;
using VideoIndexing.Domain.Dtos;
using VideoIndexing.Domain.Entities;
using VideoIndexing.Domain.Enums;

namespace VideoIndexing.Processors {
    public interface IConceptProcessor : ITransientDependency {
        IEnumerable<Concept> BuildConceptsFromFrameHashes(EnumFramingMode mode);
        Concept BuildConceptFromDirectory(DirectoryInfo directory);
        IEnumerable<SimilarityInfo> CompareConcepts(Concept one, Concept other);
    }
}