using System;
using System.Collections.Generic;
using System.Text;
using VideoIndexing.Domain.Enums;

namespace VideoIndexing.Domain.Entities {
    public abstract class FrameHashBase {
        public EnumFramingMode Discriminator { get; set; }
        public long Id { get; set; }
        public long FrameId { get; set; }
        public long VideoId { get; set; }
        public long ConceptId { get; set; }
        public string Hash { get; set; }
    }
}
