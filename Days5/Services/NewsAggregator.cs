using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Utils;

namespace Services
{
    public static class NewsAggregator
    {
        public static void Summarize(List<NewsResult> results)
        {
            Console.WriteLine("\n===== TỔNG KẾT =====");

            int total = results.Count;
            int success = results.Count(r => r.IsSuccess);
            int chars = results.Where(r => r.IsSuccess).Sum(r => r.Content.Length);

            Logger.Info($"📰 Số nguồn thành công: {success}/{total}");
            Logger.Info($"🔤 Tổng ký tự nội dung: {chars}");
        }
    }
}
