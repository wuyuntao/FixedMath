using System;
using System.Diagnostics;

namespace FixedMath.Benchmarks
{
    public abstract class Benchmark
    {
        protected const long LoopCount = 10000000;

        protected long baseCount;

        protected long fixedCount;

        protected readonly Stopwatch baseTimer = new Stopwatch();

        protected readonly Stopwatch fixedTimer = new Stopwatch();

        public override string ToString()
        {
            return string.Format("{0}: base {1}ms / {2}ops, fixed: {3}ms / {4}ops",
                    GetType().Name,
                    baseTimer.ElapsedMilliseconds,
                    baseCount,
                    fixedTimer.ElapsedMilliseconds,
                    fixedCount);
        }

        public abstract void Run();

        protected void LoopBase(Action action)
        {
            Loop(baseTimer, ref baseCount, action);
        }

        protected void LoopFixed(Action action)
        {
            Loop(fixedTimer, ref fixedCount, action);
        }

        void Loop(Stopwatch timer, ref long count, Action action)
        {
            timer.Start();

            for (int i = 0; i < LoopCount; i++)
            {
                action();
            }

            timer.Stop();

            count += LoopCount;
        }

        public long Count
        {
            get { return baseCount; }
        }

        public TimeSpan BaseTime
        {
            get { return baseTimer.Elapsed; }
        }

        public TimeSpan FixedTime
        {
            get { return fixedTimer.Elapsed; }
        }
    }
}
