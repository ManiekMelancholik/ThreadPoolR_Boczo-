using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ThreadPoolR_Boczoń
{
    public class MainWindowModelView
    {
        public MainWindow window;
        private Canvas canvas;
        private ThreadMenager generatorsMenager;
        #region Constructors
        public MainWindowModelView() { }
      
        public MainWindowModelView(bool t)
        { 
            window = new MainWindow();
            window.Show();
            window.DataContext = this;
            canvas = window.display;
            generatorsMenager = new ThreadMenager();
           // generatorsMenager.OutputSetup(canvas); 
        }
        #endregion

        private const int MIN_THREADS = 0;
        private const int MAX_THREADS = 20;

        private const float MIN_PIXELS = 0.0F;
        private const float MAX_PIXELS = 100.0F;
        /*
         * [ _ ] <-float
         * numberOfThreads <-string
         * pixelsPerSecond <-string 
         */
        #region Binding Fields

        private int _numberOfThreads = MIN_THREADS;
        public string numberOfThreads
        {
            get
            {
                return _numberOfThreads.ToString();
            }
            set
            {
                int x;
                if (!int.TryParse(value, out x))
                    _numberOfThreads = MIN_THREADS;
                else
                {
                    _numberOfThreads = x;
                    if (x < MIN_THREADS)
                        _numberOfThreads = MIN_THREADS;
                    if (x > MAX_THREADS)
                        _numberOfThreads = MAX_THREADS;

                }
            }
        }


        private float _pixelsPerSecond = MIN_PIXELS;
        public string pixelsPerSecond
        {
            get
            {
                return _pixelsPerSecond.ToString();
            }
            set
            {
                float x;
                if (!float.TryParse(value, out x))
                    _pixelsPerSecond = MIN_PIXELS;
                else
                {
                    _pixelsPerSecond = x;
                    if (x < MIN_PIXELS)
                        _pixelsPerSecond = MIN_PIXELS;
                    if (x > MAX_PIXELS)
                        _pixelsPerSecond = MAX_PIXELS;
                }

            }
        }
        #endregion

        private ICommand _start;
        private ICommand _stop;
        private ICommand _clear;
        #region Button Commands

        public ICommand start
        {
            get
            {
                if (_start == null)
                {
                    _start = new ComandClass(
                        e =>
                        {
                            generatorsMenager.FullSetup(_numberOfThreads, _pixelsPerSecond, canvas);
                           // generatorsMenager.StartAllThreads();
                            generatorsMenager.Start();
                        },
                        ce =>
                        {
                            return true;
                        }
                        );
                }
                return _start;
            }
        }


        public ICommand stop
        {
            get
            {
                if (_stop == null)
                {
                    _stop = new ComandClass(
                        e =>
                        {
                            generatorsMenager.StopAllThreads();
                            generatorsMenager.Stop();
                        },
                        ce =>
                        {
                            return true;
                        }
                        );
                }
                return _stop;
            }
        }


        public ICommand clear
        {
            get
            {
                if (_clear == null)
                {
                    _clear = new ComandClass(
                        e =>
                        {
                            generatorsMenager.StopAllThreads();
                            generatorsMenager.Stop();
                            canvas.Children.Clear();
                        },
                        ce =>
                        {
                            return true;
                        }
                        );
                }
                return _clear;
            }
        }
        #endregion
        // private static System.Windows.Shapes.Ellipse elipse;

       // public delegate void UpdateCanvas(System.Windows.Shapes.Ellipse elipse);
        //public void DrawPoint(System.Windows.Shapes.Ellipse e)
        //{
        //    var elipse = new System.Windows.Shapes.Ellipse();
        //    elipse.Fill = Brushes.Red;
        //    System.Windows.Controls.Canvas.SetLeft(elipse, 100);
        //    System.Windows.Controls.Canvas.SetTop(elipse, 200);
        //    elipse.Width = 10;
        //    elipse.Height = 5;
            
        //    canvas.Dispatcher.Invoke(()=>{ canvas.Children.Add(elipse); });
          
                    
        //}
    }


}
   


