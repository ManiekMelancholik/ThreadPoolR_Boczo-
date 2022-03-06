using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace ThreadPoolR_Boczoń
{
    public class PointGenerator
    {
        private static System.Random _rng = new Random();

        private Brush color;
        private Thread thread;
        private delegate System.Windows.Shapes.Ellipse GenerateElipse();
        private ThreadMenager output;

        private GenerationCommand[] output1;
       
        private int amount;

        private NextIndex nextOutput;
        private int _nextOutputIndex;

        private int nextOutputIndex
        {
            get
            {
                _nextOutputIndex = nextOutput.Invoke(_nextOutputIndex);
                return _nextOutputIndex;
            }
        }
        private bool endGeneration;
        
        #region Contructors
        public PointGenerator(Color color, ThreadMenager output, GenerationCommand[] output1)
        {
          
            this.color = new SolidColorBrush(color);
            this.output = output;
            this.output1 = output1;

            
            endGeneration = false;
        }
        #endregion
        /*
         * MAIN  METHOD EXECUTED BY THREAD - GENERATES DELEGATES 
         */
        
        private void GeneratePoint()
        {
            //double x;
            //double y;
            //x = _rng.NextDouble();
            //y = _rng.NextDouble();
            // int i = 0;

            //while (!endGeneration)
            for (int i = 0; i < amount; i++)
            {
                if (endGeneration)
                {
                    break;
                }
                //x = rng.NextDouble();
                //y = rng.NextDouble();

                //output.AddPixelGenerationCommand(
                output1[nextOutputIndex] =
                    (double width, double height) =>
                    {
                        System.Windows.Shapes.Ellipse ellipse = new System.Windows.Shapes.Ellipse();
                        ellipse.Fill = color;
                        ellipse.Width = 10;
                        ellipse.Height = 10;
                        System.Windows.Controls.Canvas.SetLeft(ellipse, _rng.NextDouble() * width);
                        System.Windows.Controls.Canvas.SetTop(ellipse, _rng.NextDouble() * height);
                        return ellipse;
                    };
                    
                //);
                Thread.Sleep(1);
            }
        }

        /*
         * StartGeneration()
         * StopGeneration
         */
        #region Thread operations 
        public void StartGeneration(NextIndex indGen, int amount, int start) 
        {
           _nextOutputIndex = start;
            //    this.shift = shift;
            this.amount = amount;
            nextOutput = indGen;
            if (thread == null)
            {
                thread = new Thread(GeneratePoint);
                thread.IsBackground = true;
                {
                    try
                    {
                        thread.Start();
                    }
                    catch (ThreadStartException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    return;
                }
            }
        }

        public void StopGeneration()
        {

            endGeneration = true;
            thread = null;
            
        }
        
        #endregion

    }
}
