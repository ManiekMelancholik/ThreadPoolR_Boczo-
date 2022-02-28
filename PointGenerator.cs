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
        
        #region Contructors
        public PointGenerator(Color color)
        {
            this.color = new SolidColorBrush(color);
            thread = new Thread(GeneratePoint);
            thread.IsBackground = true;
        }
        #endregion
        /*
         * MAIN  METHOD EXECUTED BY THREAD - GENERATES DELEGATES 
         */
        private void GeneratePoint()
        {
            System.Random rng = new Random();
            
            while (thread.IsAlive)
            {
                 
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
            if (thread.ThreadState == ThreadState.Unstarted)
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
