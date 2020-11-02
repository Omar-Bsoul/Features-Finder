using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VideoIndexing.Domain.Entities;
using VideoIndexing.Domain.Enums;
using VideoIndexing.Infrastructure.EntityFrameworkCore;

namespace VideoIndexing.Infrastructure.Queries {
    public class FrameHashesQueries : IFrameHashesQueries {
        public IEnumerable<FrameHashBase> GetAllFrameHashes(EnumFramingMode mode) {
            using var context = VideoIndexingDbContext.Create();

            var query = from frameHash in context.FrameHashes
                        where frameHash.Discriminator == mode
                        select frameHash;

            return query.ToList();
        }
    }
}
