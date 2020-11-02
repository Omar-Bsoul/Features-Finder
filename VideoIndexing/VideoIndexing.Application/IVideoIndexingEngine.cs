using IoCManager.Dependency;
using System.Collections.Generic;
using VideoIndexing.Domain.Dtos;
using VideoIndexing.Domain.Entities;
using VideoIndexing.Domain.Enums;

namespace VideoIndexing.Application {
    public interface IVideoIndexingEngine : ITransientDependency {
        Concept BuildConceptFromDirectory(string conceptPath);
        IEnumerable<SimilarityInfo> FindSimilarVideos(EnumFramingMode mode, Concept concept);
    }
}