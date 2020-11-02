using IoCManager.Dependency;
using System.Collections.Generic;
using System.IO;
using VideoIndexing.Domain.Dtos;
using VideoIndexing.Domain.Entities;

namespace VideoIndexing.Processors {
    public interface IVideoProcessor : ITransientDependency {
        Video BuildVideoFromDirectory(DirectoryInfo directoryInfo);
        IEnumerable<SimilarityInfo> CompareVideos((long conceptId, Video video) one, (long conceptId, Video video) other);
    }
}