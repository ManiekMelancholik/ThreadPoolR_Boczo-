using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ThreadPoolR_Boczoń
{
    public class MainWindowModelView
    {
        private MainWindow window;
        public MainWindowModelView() { }
        public MainWindowModelView(bool t)
        {
            window = new MainWindow();
            window.Show();
            window.DataContext = this;
        }


        private const int MIN_THREADS = 0;
        private const int MAX_THREADS = 20;

        private const float MIN_PIXELS = 0.0F;
        private const float MAX_PIXELS = 100.0F;
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
                           // MessageBox.Show("START CLICKED");
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

        
    }


}
