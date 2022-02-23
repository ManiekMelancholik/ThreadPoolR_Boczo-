using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace ThreadPoolR_Boczoń
{
    public class PointData
    {
        double x;
        double y;
        Ellipse point;
        public PointData(double x, double y, System.Windows.Media.Brush color)
        {
            this.x = x;
            this.y = y;
            point = new Ellipse();
            point.Width = 10;
            point.Height = 10;
            point.Fill = color;
        }
        public Ellipse SetToCanvas(double width, double height)
        {
            System.Windows.Controls.Canvas.SetLeft(point, width * x);
            System.Windows.Controls.Canvas.SetTop(point, height * y);
            return point;
        }
    }
}
