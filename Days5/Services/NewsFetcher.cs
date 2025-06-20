using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models;
using Utils;
using NewsAnalyzer;
namespace Services
{
    public static class NewsFetcher
    {
        public static async Task<List<NewsResult>> FetchAllAsync(CancellationToken token)
        {
            var results = new List<NewsResult>();
            var tasks = new List<Task>();

            foreach (var source in NewsSources.Sources)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var result = await FetchSingleAsync(source, token);
                    if (result != null) lock (results) results.Add(result);
                }));
            }

            await Task.WhenAll(tasks);
            return results;
        }

        private static async Task<NewsResult?> FetchSingleAsync(string source, CancellationToken token)
        {
            var start = DateTime.Now;

            try
            {
                Logger.Info($"Đang tải từ {source}...");
                token.ThrowIfCancellationRequested();

                if (source == "CNN")
                    throw new HttpRequestException("Lỗi kết nối đến CNN");

                await Task.Delay(new Random().Next(1500, 4000), token);
                token.ThrowIfCancellationRequested();

                var content = $"[Tin từ {source}] Lorem ipsum dolor sit amet...";
                var end = DateTime.Now;
                Logger.Success($"Tải {source} thành công ({(end - start).TotalSeconds:0.00}s)");

                return new NewsResult { Source = source, Content = content };
            }
            catch (HttpRequestException ex)
            {
                Logger.Error($"❌ {source}: {ex.Message}");
            }
            catch (OperationCanceledException)
            {
                Logger.Warn($"⏹️ Hủy tải từ {source}");
            }
            catch (Exception ex)
            {
                Logger.Error($"❗ {source}: {ex.Message}");
            }

            return null;
        }
    }
}
