using System;
using System.Threading;
using Services;
using Utils;

namespace NewsAnalyzer
{
    class Program
    {
        static CancellationTokenSource cts = new();

        static async Task Main()
        {
            while (true)
            {
                Console.WriteLine("\n===== MENU =====");
                Console.WriteLine("1. Bắt đầu tải tin tức");
                Console.WriteLine("2. Hủy tải");
                Console.WriteLine("3. So sánh Thread vs Task");
                Console.WriteLine("4. Thoát");
                Console.Write("Chọn: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        cts = new CancellationTokenSource();
                        var results = await NewsFetcher.FetchAllAsync(cts.Token);
                        NewsAggregator.Summarize(results);
                        break;
                    case "2":
                        cts.Cancel();
                        Logger.Warn("Đã gửi yêu cầu huỷ.");
                        break;
                    case "3":
                        PerformanceTester.CompareThreadVsTask();
                        break;
                    case "4":
                        return;
                    default:
                        Logger.Warn("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }
    }
}
