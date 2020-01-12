using Microsoft.Extensions.Hosting;
using Rtl.TVMaze.Domain.Model;
using Rtl.TVMaze.Service.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rtl.TVMaze.Service.BackgroundServices
{
    /// <summary>
    /// Timed service to get Shows from the TVMaze api. 
    /// When this app is scaled to multiple instances this service should be moved to a seperate app or Azure function.
    /// </summary>
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

        public TvMazeScraperHostedService(IMetaDataScraper tvMazeScraper, IStorageService storageService)
        {
            this.tvMazeScraper = tvMazeScraper;
            this.storageService = storageService;
        }

        //TODO implement update strategy, so only import new shows that are not in our DB.
        private void ScrapeShows(object state)
        {
            //get only shows from page 0 to 9 because of rate limit
            for (int i = 0; i < 10; i++)
            {
                var shows = tvMazeScraper.GetShows(i).ToList();
                tvMazeScraper.EnrichShows(shows);

                shows.ForEach(s => storageService.CreateItemAsync(s).Wait());
            }
        }

        private readonly IMetaDataScraper tvMazeScraper;
        private readonly IStorageService storageService;
        private Timer timer;
    }
}
