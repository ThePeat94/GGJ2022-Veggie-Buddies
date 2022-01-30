using System;
using System.Diagnostics;

namespace Nidavellir
{
    public class LevelTimer
    {
        private static LevelTimer s_instance;

        public static LevelTimer Instance 
        {
            get
            {
                if (s_instance == null)
                    s_instance = new LevelTimer();

                return s_instance;
            }
        }
        private Stopwatch m_stopwatch;

        private LevelTimer()
        {
            this.m_stopwatch = new();
        }
        

        public TimeSpan PastTimeSinceStart => this.m_stopwatch.Elapsed;
        
        public void StartStopWatch()
        {
            this.m_stopwatch.Start();
        }

        public void RestartStopWatch()
        {
            this.m_stopwatch.Reset();
            this.m_stopwatch.Start();
        }

        public void StopStopWatch()
        {
            this.m_stopwatch.Stop();
        }
    }
}