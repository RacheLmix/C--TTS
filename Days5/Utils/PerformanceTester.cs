using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Utils
{
    public static class PerformanceTester
    {
        public static void CompareThreadVsTask()
        {
            void DoWork(string tag)
            {
                Logger.Info($"{tag} bắt đầu");
                Thread.Sleep(3000);
                Logger.Success($"{tag} hoàn thành");
            }

            Console.WriteLine("\n===== Thread =====");
            var t1 = new Thread(() => DoWork("Thread 1"));
            var t2 = new Thread(() => DoWork("Thread 2"));
            var t3 = new Thread(() => DoWork("Thread 3"));

            var sw1 = Stopwatch.StartNew();
            t1.Start(); t2.Start(); t3.Start();
            t1.Join(); t2.Join(); t3.Join();
            sw1.Stop();

            Logger.Info($"⏱️ Tổng thời gian Thread: {sw1.Elapsed.TotalSeconds:0.00}s");

            Console.WriteLine("\n===== Task =====");
            var sw2 = Stopwatch.StartNew();
            var tasks = new[]
            {
                Task.Run(() => DoWork("Task 1")),
                Task.Run(() => DoWork("Task 2")),
                Task.Run(() => DoWork("Task 3")),
            };
            Task.WaitAll(tasks);
            sw2.Stop();

            Logger.Info($"⏱️ Tổng thời gian Task: {sw2.Elapsed.TotalSeconds:0.00}s");
        }
    }
}
