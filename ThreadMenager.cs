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
        
        private static int iterator = 0;
        private static float perSec;

        private delegate System.Windows.Shapes.Ellipse GenerationCommand();

        private static object _addPointLocker = new object();

        

        private static List<GenerationCommand> generationCommands1 = new List<GenerationCommand>();
        private static List<GenerationCommand> generationCommands2 = new List<GenerationCommand>();

        private static List<GenerationCommand> currentList = new List<GenerationCommand>();
        
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

        private static System.Windows.Controls.Canvas output;
        private static List<PointGenerator> _GENERATORS;

        #region Class Setup
        public static void CreateGenerators(int number)
        {
            for(int i = 0; i>number; i++)
            {
                _GENERATORS.Add(new PointGenerator(_THREAD_COLORS[i]));
            }
        }
        public static bool OutputSetup(object outputControl)
        {
            if (outputControl.GetType() == typeof(System.Windows.Shapes.Ellipse))
            {
                output = (System.Windows.Controls.Canvas)outputControl;
                return true;
            }
            return false;
        }

        #endregion

        /*
         * StartAllThreads()
         * EndAllThreads();
         * StopAllThreads();
         */
        #region Generators menagement

        public static void StopAllThreads()
        {
            foreach (PointGenerator PG in _GENERATORS)
                PG.StopGeneration();
        }
        public static void EndAllThreads()
        {
            foreach (PointGenerator PG in _GENERATORS)
                PG.EndGeneration();
        }
        public static void StartAllThreads()
        {
            foreach (PointGenerator PG in _GENERATORS)
                PG.StartGeneration();
        }

        #endregion


        private static Thread ThreadPoolMainThread;
        /*
         * Start()
         * Stop()
         * End()
         */
        #region Thread menagement

        public static void Start()
        {
            if(ThreadPoolMainThread == null)
            {
                ThreadPoolMainThread = new Thread(PixelDrawing);
                try
                {
                    ThreadPoolMainThread.Start();
                }
                catch(ThreadStartException e)
                {
                    MessageBox.Show(e.Message);
                }
                return;
            }

            if(ThreadPoolMainThread.ThreadState == ThreadState.Unstarted)
            {
                try
                {
                    ThreadPoolMainThread.Start();
                }
                catch (ThreadStartException e)
                {
                    MessageBox.Show(e.Message);
                }
                return;
            }

            if (ThreadPoolMainThread.ThreadState == ThreadState.Running)
            {
                return;
            }

            if(ThreadPoolMainThread.ThreadState == ThreadState.Suspended)
            {
                try
                {
                    ThreadPoolMainThread.Resume();
                }
                catch (ThreadStartException e)
                {
                    MessageBox.Show(e.Message);
                }
                return;
            }


        }
        public static void Stop()
        {
            ThreadPoolMainThread.Suspend();
        }
        public static void End()
        {
            try
            {
                ThreadPoolMainThread.Abort();
            }
            catch (ThreadAbortException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion

        public delegate void del();
        private static void PixelDrawing()
        {
            Brush brush = new SolidColorBrush(RED);
            Random r = new Random();
          
            GenDelegate();
            Thread.Sleep(1000);
           
            output.Dispatcher.Invoke(() => {

                output.Children.Add(list[0].Invoke()); 
            });
          
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
       
       
    }
}
