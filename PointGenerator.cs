using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ThreadPoolR_Boczoń
{
    public class PointGenerator
    {
        private Color color;
        private List<PointData> collection;
        private Thread thread;
        private long sleepTime;
        public PointGenerator(Color color, List<PointData> collection, float sleep)
        {
            this.color = color;
            this.collection = collection;
            sleepTime = (long) sleep*TimeSpan.TicksPerMillisecond;
            thread = new Thread(GeneratePoint);
            thread.IsBackground = true;
        }

        private void GeneratePoint()
        {
            long start,
                current;

            start = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            System.Random rng = new Random();
            
            while (thread.IsAlive)
            {
                current= DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                if (start + sleepTime <= current)
                {

                    PointData point = new PointData(
                        rng.NextDouble(),
                        rng.NextDouble(),
                        color
                        );

                    collection.Add(point);

                    start = current;
                }
            }
        }

        public void StartGeneration() 
        {
            try
            {
                thread.Start();
            }
            catch (ThreadStartException e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void StopGeneration()
        {
            try
            {
                thread.Suspend();
            }
            catch(ThreadStateException e)
            {
                MessageBox.Show(e.Message);
            }
        }


    }
}
