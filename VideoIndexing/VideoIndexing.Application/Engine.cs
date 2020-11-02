using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using VideoIndexing.Domain.Dtos;
using VideoIndexing.Domain.Entities;
using VideoIndexing.Domain.Enums;
using VideoIndexing.Processors;

namespace VideoIndexing.Application {
    public class Engine : IVideoIndexingEngine {
        public const int SimilarityThreshold = 25;

        private readonly IConceptProcessor conceptProcessor;

        public Engine(IConceptProcessor conceptProcessor) {
            this.conceptProcessor = conceptProcessor;
        }

        public Concept BuildConceptFromDirectory(string conceptPath) {
            var directory = new DirectoryInfo(conceptPath.Replace('\\', '/'));

            return conceptProcessor.BuildConceptFromDirectory(directory);
        }

        public IEnumerable<SimilarityInfo> FindSimilarVideos(EnumFramingMode mode, Concept concept) {
            var concepts = conceptProcessor.BuildConceptsFromFrameHashes(mode);

            return concepts.SelectMany(builtConcept => conceptProcessor.CompareConcepts(concept, builtConcept))
                .GroupBy(similarity => similarity.Original.VideoId)
                .Select(groupedSimilarity => groupedSimilarity.OrderByDescending(similarity => similarity.Similarity).First())
                .OrderByDescending(similarity => similarity.Similarity)
                .Take(SimilarityThreshold)
                .ToImmutableList();
        }

        #region Unused Code
        //private IEnumerable<SimilarityInfo> FindNearestVideos(IEnumerable<Concept> dbConcepts, Concept concept) {
        //    var foundVideos = new Dictionary<long, SimilarityInfo>();
        //    var frameHash = concept.Videos[0].Frames[0].GetFeaturesVector(minHashCalculator);

        //    foreach (var dbConcept in dbConcepts) {
        //        foreach (var video in dbConcept.Videos) {
        //            foundVideos.Add(video.Id, new SimilarityInfo());

        //            foreach (var frame in video.Frames) {
        //                var diff = new SimilarityInfo {
        //                    Original = new FrameIdentificationInfo {
        //                        ConceptId = dbConcept.Id,
        //                        VideoId = video.Id,
        //                        FrameId = frame.Id,
        //                    },
        //                    Target = new FrameIdentificationInfo {
        //                        ConceptId = concept.Id,
        //                    },
        //                    Similarity = jaccardCalculator.Calculate(frame.GetFeaturesVector(minHashCalculator), frameHash),
        //                };

        //                var current = foundVideos[video.Id];

        //                foundVideos[video.Id] = current.Similarity > diff.Similarity ? current : diff;
        //            }
        //        }
        //    }

        //    return foundVideos.Values.ToList();
        //}
        #endregion
    }
}
