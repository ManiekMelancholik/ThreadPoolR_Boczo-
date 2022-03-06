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
    public class ThreadMenager
    {
        private List<GenerationCommand> generationCommands1;
        private List<GenerationCommand> generationCommands2;
        private List<GenerationCommand> readList;
        private List<GenerationCommand> writeList;

        private const int COMANDS_TABLE_LENGHT = 1500;
        private GenerationCommand[] commandsTable;
        private static NextIndex nextIndex = (int val) => {
           
            if (val >= COMANDS_TABLE_LENGHT)
                val = 0;
            return val;
        };
        private int _readerIndex;
        private int readerIndex
        {
            get
            {
                _readerIndex = nextIndex.Invoke(_readerIndex);
                return _readerIndex;
            }
            set { _readerIndex++; }
        }


        private static NextIndex lastWriter = (int val) => {
            if (val >= COMANDS_TABLE_LENGHT)
                val -= COMANDS_TABLE_LENGHT;
            return val;
        };
        private int _lastWriterIndex;
        private int lastWriterIndex
        {
            get
            {
                _lastWriterIndex = lastWriter.Invoke(_lastWriterIndex);
                return _lastWriterIndex;
            }
        }
        

        private bool endDisplay;
        /*
         * ThreadMenager()
         */
        #region Constructors
        public ThreadMenager()
        {
            generationCommands1 = new List<GenerationCommand>();
            generationCommands2 = new List<GenerationCommand>();
            readList = generationCommands1;
            writeList = generationCommands1;


            commandsTable = new GenerationCommand[COMANDS_TABLE_LENGHT];
            _readerIndex = 0;
            _lastWriterIndex = 0;



           // GetPixelLock = new object();
           
        }
        #endregion

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
        

        private System.Windows.Controls.Canvas output;
        private List<PointGenerator> _GENERATORS;
        //public delegate System.Windows.Shapes.Ellipse GenerationCommand(double width, double height);
        private float tempo;
        private int reserve;
        #region Class Setup
        private void CreateGenerators(int number)
        {
            _GENERATORS = new List<PointGenerator>();
            for(int i = 0; i<number; i++)
            {
                _GENERATORS.Add(new PointGenerator(THREAD_COLORS[i], this, commandsTable));
            }
        }
        private bool OutputSetup(object outputControl)
        {
            if (outputControl.GetType() == typeof(System.Windows.Controls.Canvas))
            {
                output = (System.Windows.Controls.Canvas)outputControl;
                return true;
            }
            return false;
        }
        private void SetTempo(float tempo)
        {
          
            this.tempo = tempo;
        }

        #endregion
        public void FullSetup(int amount, float tempo, object outputC)
        {
            this.SetTempo(tempo);
            this.OutputSetup(outputC);
            this.CreateGenerators(amount);
            reserve = (5 * ((int)tempo / amount) + 1);
            
        }



        private void PixelDrawing()
        {
            Thread.Sleep(1000);
            //while (writeList.Count < 4)
            //{

            //}
            // for (int i = 0; i < 100; i++) {
            //int i = 0;
            //int reserved = 
            while (!endDisplay) {
                if (readerIndex + 2> lastWriterIndex)
                    StartAllThreads(reserve);

                output.Dispatcher.Invoke(() =>
                {
                    output.Children.Add(commandsTable[readerIndex].Invoke(output.Width, output.Height));
                
                });
                readerIndex++;
                Thread.Sleep((int)(1000/ tempo));
                //i++;
            }

        }
        private object _PixelLock;
        private object GetPixelLock
        {
            get
            {
                if (_PixelLock == null)
                    _PixelLock = new object();
                return _PixelLock;
            }
        }
        public void AddPixelGenerationCommand(GenerationCommand command)
        {

            lock (GetPixelLock)
            {
                writeList.Add(command);
            }
        }

        /*
         * StartAllThreads()
         * StopAllThreads()
         */
        #region Generators menagement

        public void StopAllThreads()
        {
            foreach (PointGenerator PG in _GENERATORS)
                PG.StopGeneration();
        }
       
        public void StartAllThreads(int amount)
        {
           // foreach (PointGenerator PG in _GENERATORS)
                for (int i = 0; i < _GENERATORS.Count; i++)
                    _GENERATORS[i].StartGeneration(
                        (int value) => { 
                            value += _GENERATORS.Count; 
                            return (value >= COMANDS_TABLE_LENGHT) ? value - COMANDS_TABLE_LENGHT : value; 
                        }, 
                        amount, 
                        lastWriterIndex + i - _GENERATORS.Count//because of lambda function above;
                    );
        }

        #endregion

        private Thread ThreadPoolMainThread;
        /*
         * Start()
         * Stop()
         */
        #region Thread menagement

        public void Start()
        {
            if(ThreadPoolMainThread == null)
            {
                ThreadPoolMainThread = new Thread(PixelDrawing);
                ThreadPoolMainThread.IsBackground = true;
                endDisplay = false;
                StartAllThreads(reserve);
                _lastWriterIndex += _GENERATORS.Count * reserve;
                //Thread.Sleep(5000);
                //StartAllThreads(reserve);
                //StartAllThreads(10);
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


            if (ThreadPoolMainThread.ThreadState == ThreadState.Running)
            {
                return;
            }

        }
        public void Stop()
        {
            endDisplay = true;
            ThreadPoolMainThread = null;
        }


        #endregion







        //private delegate System.Windows.Shapes.Ellipse TestDeleg();
        //private List<TestDeleg> list = new List<TestDeleg>();
        //public void GenDelegate()
        //{
        //    Thread tt = new Thread(
        //            () =>
        //            {
        //                list.Add(new TestDeleg(
        //                        () =>
        //                        {
        //                            var elipse = new System.Windows.Shapes.Ellipse();
        //                            elipse.Fill = Brushes.Red;
        //                            System.Windows.Controls.Canvas.SetLeft(elipse, 100);
        //                            System.Windows.Controls.Canvas.SetTop(elipse, 200);
        //                            elipse.Width = 10;
        //                            elipse.Height = 5;
        //                            return elipse;
        //                        }
        //                        ));
        //            }
        //        );
        //    tt.Start();
        //}
       
       
    }
}
