using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ThreadPoolR_Boczoń
{
    public static class ThreadMenager
    {
        private static Thread ThreadPoolMainThread;
        private static int iterator = 0;
        private static List<PointData> COLLECTION_OF_POINTS = new List<PointData>();
        private static object _addPointLocker = new object();
        private static List<Color> _THREAD_COLORS;
        private static System.Windows.Controls.Canvas canvas;
        private static float perSec;
       // public delegate void UpdateCanvas(System.Windows.Shapes.Ellipse elipse);


        #region Colors Initialisation
        private static List<Color> THREAD_COLORS
        {
            get
            {
                if (_THREAD_COLORS == null)
                {
                    _THREAD_COLORS = new List<Color>();
                    INITIALISE_COLORS();
                }
                return _THREAD_COLORS;
            }
        }


        //Colors pallete idea
        //https://sashamaps.net/docs/resources/20-colors/


        private static Color RED = Color.FromRgb(230, 25, 75);
        private static Color GREEN = Color.FromRgb(60, 180, 75);
        private static Color YELLOW = Color.FromRgb(255, 225, 25);
        private static Color BLUE = Color.FromRgb(0, 130, 200);
        private static Color ORANGE = Color.FromRgb(245, 130, 48);
        private static Color PURPLE = Color.FromRgb(145, 30, 180);
        private static Color CYAN = Color.FromRgb(70, 240, 240);
        private static Color MAGENTA = Color.FromRgb(240, 50, 230);
        private static Color LIME = Color.FromRgb(210, 245, 60);
        private static Color PINK = Color.FromRgb(250, 190, 212);
        private static Color TEAL = Color.FromRgb(0, 128, 128);
        private static Color LAVENDER = Color.FromRgb(220, 190, 255);
        private static Color BROWN = Color.FromRgb(170, 110, 40);
        private static Color BEIGE = Color.FromRgb(255, 250, 200);
        private static Color MAROON = Color.FromRgb(128, 0, 0);
        private static Color MINT = Color.FromRgb(170, 255, 195);
        private static Color OLIVE = Color.FromRgb(128, 128, 0);
        private static Color APRICOT = Color.FromRgb(255, 215, 180);
        private static Color NAVY = Color.FromRgb(0, 0, 128);
        private static Color GREY = Color.FromRgb(128, 128, 128);

        private static void INITIALISE_COLORS()
        {
            _THREAD_COLORS.Add(RED);
            _THREAD_COLORS.Add(GREEN);
            _THREAD_COLORS.Add(YELLOW);
            _THREAD_COLORS.Add(BLUE);
            _THREAD_COLORS.Add(ORANGE);
            _THREAD_COLORS.Add(PURPLE);
            _THREAD_COLORS.Add(CYAN);
            _THREAD_COLORS.Add(MAGENTA);
            _THREAD_COLORS.Add(LIME);
            _THREAD_COLORS.Add(PINK);
            _THREAD_COLORS.Add(TEAL);
            _THREAD_COLORS.Add(LAVENDER);
            _THREAD_COLORS.Add(BROWN);
            _THREAD_COLORS.Add(BEIGE);
            _THREAD_COLORS.Add(MAROON);
            _THREAD_COLORS.Add(MINT);
            _THREAD_COLORS.Add(OLIVE);
            _THREAD_COLORS.Add(APRICOT);
            _THREAD_COLORS.Add(NAVY);
            _THREAD_COLORS.Add(GREY);
        }

        #endregion

        static MainWindowModelView mWMV;
        public delegate void del();
        private static void PixelDrawing()
        {
            Brush brush = new SolidColorBrush(RED);
            Random r = new Random();
            // COLLECTION_OF_POINTS.Add(new PointData(r.NextDouble(), r.NextDouble(), brush));
            GenDelegate();
            Thread.Sleep(1000);
            //canvas.Children.Add(elipse);
            canvas.Dispatcher.Invoke(() => {

                //var elipse = new System.Windows.Shapes.Ellipse();
                //elipse.Fill = Brushes.Red;
                //System.Windows.Controls.Canvas.SetLeft(elipse, 100);
                //System.Windows.Controls.Canvas.SetTop(elipse, 200);
                //elipse.Width = 10;
                //elipse.Height = 5;

                canvas.Children.Add(list[0].Invoke()); });
           // canvas.Dispatcher.BeginInvoke((Action)(() => { mWMV.DrawPoint(elipse); }));



        }
        private delegate System.Windows.Shapes.Ellipse TestDeleg();
        private static List<TestDeleg> list = new List<TestDeleg>();
        public static void GenDelegate()
        {
            Thread tt = new Thread(
                
                    () =>
                    {
                        list.Add(new TestDeleg(
                                () =>
                                {
                                    var elipse = new System.Windows.Shapes.Ellipse();
                                    elipse.Fill = Brushes.Red;
                                    System.Windows.Controls.Canvas.SetLeft(elipse, 100);
                                    System.Windows.Controls.Canvas.SetTop(elipse, 200);
                                    elipse.Width = 10;
                                    elipse.Height = 5;
                                    return elipse;
                                }
                            ));
                    }
                
                );
            tt.Start();
        }
       public static void Setup(System.Windows.Controls.Canvas c)
        {
            canvas = c;
        }
        public static void TestStart(System.Windows.Controls.Canvas c,MainWindowModelView mv)
        {
            //mWMV=mv;
            //canvas = c;
            perSec = 4;
            ThreadPoolMainThread = new Thread(PixelDrawing);
            ThreadPoolMainThread.SetApartmentState(ApartmentState.STA);
            ThreadPoolMainThread.IsBackground = true;
            ThreadPoolMainThread.Start();
           
        }
        public static void GenSomePoints()
        {
            Brush brush = new SolidColorBrush(RED);
            Random r = new Random();
            for (int i = 0; i < 100; i++)
                COLLECTION_OF_POINTS.Add(new PointData(r.NextDouble(), r.NextDouble(), brush));
        }
    }
}
