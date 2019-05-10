using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCluster
{
    public class Handler
    {
        public static List<ClusterColor> RandomColor()
        {
            Random rand = new Random();
            int colorCount = 5;
            List<ClusterColor> colorList = new List<ClusterColor>(colorCount);

            for(int i = 0; i < colorCount; i++)
            {
                byte r = (byte)rand.Next(255),
                     g = (byte)rand.Next(255),
                     b = (byte)rand.Next(255);
                double percent = rand.Next(100);
                ClusterColor clusterColor = new ClusterColor(r, g, b, percent);
                colorList.Add(clusterColor);
            }

            return colorList;
        }
    }
}
