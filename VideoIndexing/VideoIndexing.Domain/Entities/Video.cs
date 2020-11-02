using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoIndexing.Domain.Entities {
    public class Video {
        public long Id { get; set; }
        public List<Frame> Frames { get; set; }

        public Video() : this(0, null) {
        }

        public Video(long id) : this(id, null) {
        }

        public Video(long id, List<Frame> frames) {
            Id = id;
            Frames = frames ?? new List<Frame>();
        }

        public void AddFrame(Frame frame) {
            Frames.Add(frame);
        }
    }
}
