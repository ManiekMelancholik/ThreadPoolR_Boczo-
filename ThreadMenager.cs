using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ThreadPoolR_Boczoń
{
    public static class ThreadMenager
    {
        private static Thread ThreadPoolMainThread = new Thread(PixelDrawing);
        private static int iterator = 0;
        private static List<PointData> COLLECTION_OF_POINTS = new List<PointData>();
        private static bool COLLECTION_IN_USE = false;
        private static List<Color> _THREAD_COLORS;
        private static System.Windows.Controls.Canvas canvas;
        private static float perSec;
        public static bool REQUEST_ACCESS()
        {
            if (!COLLECTION_IN_USE)
            {
                COLLECTION_IN_USE = true;
                return true;
            }

            return false;
        }
        public static void RELEASE_ACCESS()
        {
            COLLECTION_IN_USE = false;
        }

        private static void PixelDrawing()
        {

            while (ThreadPoolMainThread.IsAlive)
            {
                if (COLLECTION_OF_POINTS.Count>iterator) {
                    canvas.Children.Add(
                        COLLECTION_OF_POINTS[iterator].
                            SetToCanvas(canvas.Width, canvas.Height)
                    );
                    iterator++;
                    Thread.Sleep((int)(TimeSpan.TicksPerSecond / perSec));
                }
            }

        }
    }
}
