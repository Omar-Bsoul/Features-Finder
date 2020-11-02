using System.Collections.Generic;
using System.IO;
using VideoIndexing.Domain.Dtos;
using VideoIndexing.Domain.Entities;

namespace VideoIndexing.Processors {
    public class VideoProcessor : IVideoProcessor {
        private readonly IFrameProcessor frameProcessor;

        public VideoProcessor(IFrameProcessor frameProcessor) {
            this.frameProcessor = frameProcessor;
        }

        public Video BuildVideoFromDirectory(DirectoryInfo directory) {
            var videoFramesFiles = new Queue<FileInfo>(directory.GetFiles());

            var frames = new List<Frame>();

            while (videoFramesFiles.Count != 0) {
                frames.Add(frameProcessor.BuildFrameFromFile(videoFramesFiles.Dequeue()));
            }

            return new Video() {
                Id = ResolveVideoId(directory),
                Frames = frames,
            };
        }

        public IEnumerable<SimilarityInfo> CompareVideos((long conceptId, Video video) one, (long conceptId, Video video) other) {
            var similarities = new List<SimilarityInfo>();

            foreach (var oneFrame in one.video.Frames) {
                foreach (var otherFrame in other.video.Frames) {
                    similarities.Add(frameProcessor.CompareFrames((one.conceptId, one.video.Id, oneFrame), (other.conceptId, other.video.Id, otherFrame)));
                }
            }

            return similarities;
        }

        private long ResolveVideoId(DirectoryInfo directory) {
            return long.Parse(directory.Name);
        }
    }
}