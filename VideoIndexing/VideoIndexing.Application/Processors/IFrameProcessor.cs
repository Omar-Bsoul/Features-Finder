using IoCManager.Dependency;
using System.Collections.Generic;
using System.IO;
using VideoIndexing.Domain.Dtos;
using VideoIndexing.Domain.Entities;

namespace VideoIndexing.Processors {
    public interface IFrameProcessor : ITransientDependency {
        Frame BuildFrameFromFile(FileInfo fileInfo);
        SimilarityInfo CompareFrames((long conceptId, long videoId, Frame frame) one, (long conceptId, long videoId, Frame frame) other);
    }
}