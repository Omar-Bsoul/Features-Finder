using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using VideoIndexing.Domain.Services;
using VideoIndexing.Domain.Utils;

namespace VideoIndexing.Domain.Entities {
    public class Frame {
        public long Id { get; set; }
        public List<MKeyPoint> KazePoints { get; set; }

        public Frame() : this(0, null) {
        }

        public Frame(long id) : this(id, null) {
        }

        public Frame(long id, List<MKeyPoint> kazePoints) {
            Id = id;
            KazePoints = kazePoints;
        }

        public IEnumerable<uint> GetFeaturesVector(IMinHashCalculator minHashCalculator) {
            var featuresVector = new List<int>();

            foreach (var point in KazePoints) {
                featuresVector.Add(ConvertFloatToInt(point.Angle));
                featuresVector.Add(ConvertFloatToInt(point.ClassId));
                featuresVector.Add(ConvertFloatToInt(point.Octave));
                featuresVector.Add(ConvertFloatToInt(point.Point.X));
                featuresVector.Add(ConvertFloatToInt(point.Point.Y));
                featuresVector.Add(ConvertFloatToInt(point.Response));
                featuresVector.Add(ConvertFloatToInt(point.Size));
            }

            return minHashCalculator.Calculate(20, 100, featuresVector);
        }

        public void SetFeaturesVector(IEnumerable<uint> value) {
            KazePoints.Clear();

            for (int i = 0; i < value.Count() / 7; i++) {
                MKeyPoint keyPoint = new MKeyPoint {
                    Angle = value.ElementAt(i + 0),
                    ClassId = (int)value.ElementAt(i + 1),
                    Octave = (int)value.ElementAt(i + 2),
                    Point = new System.Drawing.PointF {
                        X = value.ElementAt(i + 3),
                        Y = value.ElementAt(i + 4),
                    },
                    Response = value.ElementAt(i + 5),
                    Size = value.ElementAt(i + 6),
                };

                KazePoints.Add(keyPoint);
            }
        }

        public double CalculateDistance(Frame frame, IMinHashCalculator minHashCalculator, IJaccardCalculator jaccardCalculator) {
            var thisFeatureVector = GetFeaturesVector(minHashCalculator);
            var otherFeatureVector = frame.GetFeaturesVector(minHashCalculator);

            return jaccardCalculator.Calculate(thisFeatureVector, otherFeatureVector);
        }

        private int ConvertFloatToInt(double number) {
            try {
                return int.Parse(number.ToString().Replace(".", ""));
            } catch {
                return int.MaxValue;
            }
        }
    }
}
