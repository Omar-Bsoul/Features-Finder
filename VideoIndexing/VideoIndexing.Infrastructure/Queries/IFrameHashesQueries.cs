using IoCManager.Dependency;
using System.Collections.Generic;
using VideoIndexing.Domain.Entities;
using VideoIndexing.Domain.Enums;

namespace VideoIndexing.Infrastructure.Queries {
    public interface IFrameHashesQueries : ITransientDependency {
        IEnumerable<FrameHashBase> GetAllFrameHashes(EnumFramingMode mode);
    }
}
