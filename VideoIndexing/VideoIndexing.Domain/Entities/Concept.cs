using System;
using System.Collections.Generic;

namespace VideoIndexing.Domain.Entities {
    public class Concept {
        public long Id { get; set; }
        public List<Video> Videos { get; set; }

        public Concept() : this(0, null) {
        }

        public Concept(long id) : this(id, null) {
        }

        public Concept(long id, List<Video> videos) {
            Id = id;
            Videos = videos ?? new List<Video>();
        }

        public void AddVideo(Video video) {
            Videos.Add(video);
        }
    }
}
