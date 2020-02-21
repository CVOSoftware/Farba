using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using System.Linq;

namespace Farba.Common.Clusters
{
    public class Kmeans : IDisposable
    {
        #region Const

        private const int ITERATION_COUNT = 20;

        private const int SCALABLE_WIDTH = 200;

        private const int SCALABLE_HEIGHT = 200;

        private const int ALPHA = 255;

        #endregion

        #region Fields

        private int ClusterCount;

        private Bitmap BitmapData;

        private List<Cluster> Clusters;

        #endregion

        #region Constructor

        public Kmeans(BitmapImage bitmapImage, Guid id, int clusterCount)
        {
            Id = id;
            ClusterCount = clusterCount;
            Clusters = new List<Cluster>(ClusterCount);
            BitmapData = ConvertBitmapIMageToBitmap(bitmapImage);
        }

        #endregion

        #region Public

        public List<ClusterColor> GetClusters()
        {
            IdentifyCenters();
            Clustering();
            SortClusters();
            return ConvertClusters();
        }

        public Guid Id { get; }

        #endregion


        #region Data preparation

        private Bitmap ConvertBitmapIMageToBitmap(BitmapImage bitmapImage)
        {
            using (var stream = new MemoryStream())
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(stream);
                using (var bitmap = new Bitmap(stream))
                {
                    return BitmapScalable(bitmap);
                }

            }
        }

        private Bitmap BitmapScalable(Bitmap bitmap)
        {
            var width = bitmap.Width < SCALABLE_WIDTH ? bitmap.Width : SCALABLE_WIDTH;
            var height = bitmap.Width < SCALABLE_HEIGHT ? bitmap.Width : SCALABLE_HEIGHT;

            return new Bitmap(bitmap, width, height);
        }

        private List<ClusterColor> ConvertClusters()
        {
            var clusters = new List<ClusterColor>(ClusterCount);
            var pixels = BitmapData.Height * BitmapData.Width;

            foreach (var cluster in Clusters)
            {
                clusters.Add(cluster.GetClusterColor(pixels));
            }

            Clusters.Clear();

            return clusters;
        }

        #endregion

        #region Ahgorithm parts

        private void IdentifyCenters()
        {
            var random = new Random();
            for (var i = 0; i < ClusterCount; i++)
            {
                var x = random.Next(0, BitmapData.Width);
                var y = random.Next(0, BitmapData.Height);
                var centroid = BitmapData.GetPixel(x, y);
                var cluster = new Cluster(centroid);
                Clusters.Add(cluster);
            }
        }

        private double EuclidDistance(Color centroid, Color point)
        {
            var r = Math.Pow(centroid.R - point.R, 2);
            var g = Math.Pow(centroid.G - point.G, 2);
            var b = Math.Pow(centroid.B - point.B, 2);
            var sum = r + g + b;
            return Math.Sqrt(sum);
        }

        private void Clustering()
        {
            for (var i = 0; i < ITERATION_COUNT; i++)
            {
                for (var y = 0; y < BitmapData.Height; y++)
                {
                    for (var x = 0; x < BitmapData.Width; x++)
                    {
                        var distance = new double[ClusterCount];
                        for (var k = 0; k < ClusterCount; k++)
                        {
                            distance[k] = EuclidDistance(Clusters[k].Centroid, BitmapData.GetPixel(x, y));
                        }
                        var index = MinimumValueIndexSearch(distance);
                        Clusters[index].Vector.Add(BitmapData.GetPixel(x, y));
                    }
                }

                CalculateCentroids(i);
            }
        }

        private int MinimumValueIndexSearch(double[] distance)
        {
            var index = 0;
            for (int i = 0; i < ClusterCount; i++)
            {
                if (distance[index] > distance[i])
                {
                    index = i;
                }
            }
            return index;
        }

        private void CalculateCentroids(int iter)
        {
            for (var i = 0; i < ClusterCount; i++)
            {
                Clusters[i].Average();
                if (iter < ITERATION_COUNT - 1)
                {
                    Clusters[i].ClearVector();
                }
            }
        }

        private void SortClusters()
        {
            for (var i = 0; i < ClusterCount - 1; i++)
            {
                var breakSort = false;
                for (var j = 0; j < ClusterCount - i - 1; j++)
                {
                    if (Clusters[j + 1].Vector.Count <= Clusters[j].Vector.Count)
                    {
                        continue;
                    }
                    var temp = Clusters[j + 1];
                    Clusters[j + 1] = Clusters[j];
                    Clusters[j] = temp;
                    breakSort = true;
                }
                if (breakSort == false)
                {
                    break;
                }
            }
        }

        #endregion

        #region Dispose pattern

        private bool Disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {

                }
                BitmapData.Dispose();
                Disposed = true;
            }
        }

        ~Kmeans()
        {
            Dispose(false);
        }

        #endregion

        #region Internal type

        private class Cluster
        {
            public double Percent { get; private set; }

            public Color Centroid { get; private set; }

            public List<Color> Vector { get; }

            public Cluster(Color centroid)
            {
                Percent = 0;
                Centroid = centroid;
                Vector = new List<Color>();
            }

            public void Average()
            {
                var count = Vector.Count;
                if (count > 0)
                {
                    var r = 0;
                    var g = 0;
                    var b = 0;
                    for (var i = 0; i < count; i++)
                    {
                        r += Vector[i].R;
                        g += Vector[i].G;
                        b += Vector[i].B;
                    }
                    r /= count;
                    g /= count;
                    b /= count;
                    Centroid = Color.FromArgb(ALPHA, r, g, b);
                }
            }

            public ClusterColor GetClusterColor(int pixels)
            {
                Percent = Math.Round(100.0 * Vector.Count / pixels);
                return new ClusterColor(Percent, Centroid.R, Centroid.G, Centroid.B);
            }

            public void ClearVector()
            {
                Vector.Clear();
            }
        }

        #endregion
    }
}
