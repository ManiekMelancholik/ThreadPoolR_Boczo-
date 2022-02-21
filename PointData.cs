using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPoolR_Boczoń
{
    public class PointData
    {
        double x;
        double y;
        Color color;
        public PointData(double x, double y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
    }
}
