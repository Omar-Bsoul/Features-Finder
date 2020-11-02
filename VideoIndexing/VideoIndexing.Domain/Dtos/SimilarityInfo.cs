using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoIndexing.Domain.Dtos {
    public class SimilarityInfo {
        public FrameIdentificationInfo Original { get; set; }
        public FrameIdentificationInfo Target { get; set; }
        public double Similarity { get; set; }
    }
}
