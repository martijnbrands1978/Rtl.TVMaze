using Microsoft.Extensions.Hosting;
using Rtl.TVMaze.Domain.Model;
using Rtl.TVMaze.Service.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rtl.TVMaze.Service.BackgroundServices
{
    public class TvMazeScraperHostedService : IHostedService, IDisposable
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(ScrapeShows, null, TimeSpan.Zero,
                TimeSpan.FromDays(7));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public TvMazeScraperHostedService(IMetaDataScraper tvMazeScraper, IStorageService<Show> storageService)
        {
            this.tvMazeScraper = tvMazeScraper;
            this.storageService = storageService;
        }

        private void ScrapeShows(object state)
        {
            //get shows from page 0 to 9
            for (int i = 0; i < 2; i++)
            {
                var shows = tvMazeScraper.GetShows(i).ToList();
                tvMazeScraper.EnrichShows(shows);

                //shows.ForEach(s => storageService.CreateItemAsync(s).Wait());
            }
        }

        private readonly IMetaDataScraper tvMazeScraper;
        private readonly IStorageService<Show> storageService;
        private Timer timer;
    }
}
