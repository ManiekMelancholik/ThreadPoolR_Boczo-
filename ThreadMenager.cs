using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolR_Boczoń
{
    public static class ThreadMenager
    {
        private static Thread ThreadPoolMainThread = new Thread();
        private static List<PointData> pointsInView = new List<PointData>();
        private static List<PointData> pointsToAdd = new List<PointData>();


        private static EventHandler eventHandler = new EventHandler();


    }
}
