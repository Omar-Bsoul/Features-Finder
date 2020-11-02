using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideoIndexing.Domain.Dtos;
using VideoIndexing.Domain.Entities;
using VideoIndexing.Domain.Enums;
using VideoIndexing.Infrastructure.Queries;

namespace VideoIndexing.Processors {
    public class ConceptProcessor : IConceptProcessor {
        private readonly IFrameHashesQueries frameHashesQueries;
        private readonly IVideoProcessor videoProcessor;

        public ConceptProcessor(IFrameHashesQueries frameHashesQueries,
            IVideoProcessor videoProcessor) {
            this.frameHashesQueries = frameHashesQueries;
            this.videoProcessor = videoProcessor;
        }

        public IEnumerable<Concept> BuildConceptsFromFrameHashes(EnumFramingMode mode) {
            var frameHashes = frameHashesQueries.GetAllFrameHashes(mode);

            var parsedFrames = ParseFrameHashesIntoFrames(frameHashes);

            var concepts = parsedFrames.Item1;
            var videos = parsedFrames.Item2;
            var frames = parsedFrames.Item3;
            var hashes = parsedFrames.Item4;

            return BuildConceptsFromParsedFrames(concepts, videos, frames, hashes);
        }

        public Concept BuildConceptFromDirectory(DirectoryInfo directory) {
            var videos = new List<Video>();

            var videoDirectories = new Queue<DirectoryInfo>(directory.GetDirectories());

            while (videoDirectories.Count != 0) {
                videos.Add(videoProcessor.BuildVideoFromDirectory(videoDirectories.Dequeue()));
            }

            return new Concept() {
                Id = ResolveConceptId(directory),
                Videos = videos,
            };
        }

        public IEnumerable<SimilarityInfo> CompareConcepts(Concept one, Concept other) {
            var similarities = new List<SimilarityInfo>();

            foreach (var oneVideo in one.Videos) {
                foreach (var otherVideo in other.Videos) {
                    similarities.AddRange(videoProcessor.CompareVideos((one.Id, oneVideo), (other.Id, otherVideo)));
                }
            }

            return similarities;
        }

        private (IEnumerable<long>, IEnumerable<long>, IEnumerable<long>, IEnumerable<IEnumerable<uint>>) ParseFrameHashesIntoFrames(IEnumerable<FrameHashBase> frameHashes) {
            var concepts = new List<long>();
            var videos = new List<long>();
            var frames = new List<long>();
            var hashes = new List<IEnumerable<uint>>(); // each frame has list of paramters to represent that frame

            foreach (var frameHash in frameHashes) {
                concepts.Add(frameHash.ConceptId);
                videos.Add(frameHash.VideoId);
                frames.Add(frameHash.FrameId);

                // TODO: Convert hash column into JSON format in order to ease converting them to List<int>
                var hashParams = ParseFrameHashSequence(frameHash.Hash);
               
                hashes.Add(hashParams);
            }

            return (concepts, videos, frames, hashes);
        }

        private IEnumerable<uint> ParseFrameHashSequence(string sequence) {
            return sequence.Split(',').Select(uint.Parse);
        }

        private IEnumerable<Concept> BuildConceptsFromParsedFrames(IEnumerable<long> concepts, IEnumerable<long> videos, IEnumerable<long> frames, IEnumerable<IEnumerable<uint>> hashes) {
            var idToConcept = new Dictionary<long, Concept>();
            var idToVideo = new Dictionary<long, Video>();
            var idToFrame = new Dictionary<long, Frame>();

            for (int i = 0; i < hashes.Count(); i++) {
                Concept concept;
                if (idToConcept.ContainsKey(concepts.ElementAt(i))) {
                    concept = idToConcept[concepts.ElementAt(i)];
                } else {
                    idToConcept.Add(concepts.ElementAt(i), concept = new Concept(concepts.ElementAt(i)));
                }

                Video video;
                if (idToVideo.ContainsKey(videos.ElementAt(i))) {
                    video = idToVideo[videos.ElementAt(i)];
                } else {
                    idToVideo.Add(videos.ElementAt(i), video = new Video(videos.ElementAt(i)));
                    concept.AddVideo(video);
                }

                if (!idToFrame.ContainsKey(frames.ElementAt(i))) {
                    Frame frame = new Frame(frames.ElementAt(i));
                    frame.SetFeaturesVector(hashes.ElementAt(i));

                    video.AddFrame(frame);
                    idToFrame.Add(frames.ElementAt(i), frame);
                }
            }

            return idToConcept.Values.ToList();
        }

        private long ResolveConceptId(DirectoryInfo directory) {
            return long.Parse(directory.Name);
        }

        #region Unused Code
        //private void HashVideo(VideoProcessor videoProcessor) {
        //    MySqlCommand command = new MySqlCommand {
        //        Connection = DatabaseService.Connection
        //    };
        //
        //    try {
        //        command.Connection.Open();
        //        command.CommandText = $"SELECT KAZE, FrameID FROM kazetable WHERE VideoID='{videoProcessor.VideoId}';";
        //
        //        List<string> kazeFeatures = new List<string>();
        //        List<string> frames = new List<string>();
        //
        //        MySqlDataReader reader = command.ExecuteReader();
        //        while (reader.Read()) {
        //            kazeFeatures.Add(reader.GetString(0));
        //            frames.Add(reader.GetString(1));
        //        }
        //
        //        MinHash_Kaze(videoProcessor.VideoId, kazeFeatures, frames);
        //    } catch (Exception ex) {
        //
        //    } finally {
        //        command.Connection.Close();
        //    }
        //}

        //private void MinHash_Kaze(string videoID, List<string> kazeFeatures, List<string> frames) {
        //    List<int> inums1 = new List<int>();
        //    MinHash minHash = new MinHash(20, 100);
        //    int FrameID;
        //    while (kazeFeatures.Count != 0) {
        //        inums1 = Convert_To_Int(kazeFeatures[0]);
        //        FrameID = Convert.ToInt32(frames[0]);
        //        List<uint> hvs1 = minHash.GetMinHash(inums1).ToList();
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.Connection = DatabaseService.Connection;
        //
        //        string HashValue = "";
        //        for (int i = 0; i < hvs1.Count; i++)
        //            HashValue += hvs1[i].ToString() + ",";
        //
        //        // store hash value depends on frame id 1frame/sec or 4frame/sec 
        //
        //        if (FrameID % 6 == 0 && FrameID != 0) {
        //            cmd.CommandText = $"INSERT INTO fourframehash (ConceptID, VideoID, FrameID, Hash) VALUES ('{ConceptId}','{videoID}','{FrameID}','{HashValue}');";
        //        } else if (FrameID % 6 != 0 || FrameID == 0) {
        //            cmd.CommandText = $"INSERT INTO oneframehash (ConceptID, VideoID, FrameID, Hash) VALUES ('{ConceptId}','{videoID}','{FrameID}','{HashValue}');";
        //        }
        //
        //        try {
        //            cmd.Connection.Open();
        //            cmd.ExecuteNonQuery();
        //        } catch (Exception) {
        //
        //        } finally {
        //            cmd.Connection.Close();
        //        }
        //
        //        kazeFeatures.RemoveAt(0);
        //        frames.RemoveAt(0);
        //    }
        //}

        //private List<int> Convert_To_Int(string vid) {
        //    List<int> integers = new List<int>();
        //    string[] lines = vid.Split('\n');
        //
        //    for (int i = 0; i < lines.Length; i++) {
        //        string[] nums = lines[i].Split(',');
        //
        //        for (int j = 0; j < nums.Length; j++)
        //            if (nums[j] != "") {
        //                nums[j] = nums[j].Replace(".", "");
        //                int y = Convert.ToInt32(nums[j]);
        //                integers.Add(y);
        //            }
        //    }
        //    return integers;
        //}
        #endregion
    }
}