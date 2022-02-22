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
      //  private static Thread ThreadPoolMainThread = new Thread();
        private static List<PointData> pointsInView = new List<PointData>();
        private static List<PointData> pointsToAdd = new List<PointData>();

        private static List<Color> _THREAD_COLORS;
       
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
        private static Color YELLOW = Color.FromRgb(255,225,25);
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




    }
}
