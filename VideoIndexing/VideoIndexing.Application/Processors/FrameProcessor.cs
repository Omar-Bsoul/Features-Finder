using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using VideoIndexing.Domain.Dtos;
using VideoIndexing.Domain.Entities;
using VideoIndexing.Domain.Services;

namespace VideoIndexing.Processors {
    public class FrameProcessor : IFrameProcessor {
        private readonly IMinHashCalculator minHashCalculator;
        private readonly IJaccardCalculator jaccardCalculator;

        public FrameProcessor(IMinHashCalculator minHashCalculator,
            IJaccardCalculator jaccardCalculator) {
            this.minHashCalculator = minHashCalculator;
            this.jaccardCalculator = jaccardCalculator;
        }

        public Frame BuildFrameFromFile(FileInfo file) {
            IEnumerable<MKeyPoint> kazeKeyPoints;

            using (var frame = new Image<Bgr, byte>(file.FullName)) {
                using (var kaze = new KAZE(true, true)) {
                    kazeKeyPoints = kaze.Detect(frame);
                }
            }

            return new Frame() {
                Id = ResolveFrameId(file),
                KazePoints = kazeKeyPoints.ToList(),
            };
        }

        public SimilarityInfo CompareFrames((long conceptId, long videoId, Frame frame) one, (long conceptId, long videoId, Frame frame) other) {
            var similarity = new SimilarityInfo {
                Original = new FrameIdentificationInfo {
                    ConceptId = one.conceptId,
                    VideoId = one.videoId,
                    FrameId = one.frame.Id,
                },
                Target = new FrameIdentificationInfo {
                    ConceptId = other.conceptId,
                    VideoId = other.videoId,
                    FrameId = other.frame.Id,
                },
                Similarity = one.frame.CalculateDistance(other.frame, minHashCalculator, jaccardCalculator),
            };

            return similarity;
        }

        private long ResolveFrameId(FileInfo file) {
            return long.Parse(Path.GetFileNameWithoutExtension(file.Name));
        }
    }
}