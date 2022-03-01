using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace ThreadPoolR_Boczoń
{
    public class PointGenerator
    {

        private Brush color;
        private Thread thread;
        private delegate System.Windows.Shapes.Ellipse GenerateElipse();
        private ThreadMenager output;
        
        #region Contructors
        public PointGenerator(Color color, ThreadMenager output)
        {
          
            this.color = new SolidColorBrush(color);
            this.output = output;
            thread = new Thread(GeneratePoint);
            thread.IsBackground = true;
            
        }
        #endregion
        /*
         * MAIN  METHOD EXECUTED BY THREAD - GENERATES DELEGATES 
         */
        
        private void GeneratePoint()
        {
            //MessageBox.Show("Adding");
            System.Random rng = new Random();
            double x;
            double y;
            x = rng.NextDouble();
            y = rng.NextDouble();

            for (int i = 0; i < 20; i++)
            {
                //x = rng.NextDouble();
                //y = rng.NextDouble();
               

                output.AddPixelGenerationCommand(

                    (double width, double height) =>
                    {
                        System.Windows.Shapes.Ellipse ellipse = new System.Windows.Shapes.Ellipse();
                        ellipse.Fill = color;
                        ellipse.Width = 10;
                        ellipse.Height = 10;
                        System.Windows.Controls.Canvas.SetLeft(ellipse, rng.NextDouble() * width);
                        System.Windows.Controls.Canvas.SetTop(ellipse, rng.NextDouble() * height);
                        return ellipse;
                    }
                    
                );
                Thread.Sleep(1);
            }
        }

        /*
         * StartGeneration()
         * StopGeneration
         * EndGeneration
         */
        #region Thread operations 
        public void StartGeneration() 
        {
           // if (thread.ThreadState == ThreadState.Unstarted)
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
            if(thread.ThreadState == ThreadState.Stopped)
            {
                try
                {
                    thread.Resume();
                }
                catch (ThreadStartException e)
                {
                    MessageBox.Show(e.Message);
                }
                return;
            }
            
        }
        public void StopGeneration()
        {
           
            thread.Suspend();
            
        }
        public void EndGeneration()
        {
            try
            {
                thread.Abort();
            }
            catch(ThreadAbortException e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion

    }
}
