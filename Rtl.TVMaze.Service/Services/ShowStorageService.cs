using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Rtl.TVMaze.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rtl.TVMaze.Service.Services
{
    public class ShowStorageService : IStorageService
    {
        /// <summary>
        /// Create Show in documentDB
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Document> CreateItemAsync(Show item)
        {
            return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), item);
        }

        /// <summary>
        /// Get items from documentDB, each page returns 10 items. Shows are sorted by name.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Show>> GetItemsAsync(int page)
        {
            IDocumentQuery<Show> query = client.CreateDocumentQuery<Show>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .OrderBy(x => x.Name).Skip(page * 10).Take(10)
                .AsDocumentQuery();

            List<Show> results = new List<Show>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<Show>());
            }

            return results;
        }

        public ShowStorageService()
        {
            this.client = new DocumentClient(new Uri(Endpoint), Key);
        }

        private DocumentClient client;
        //TODO move Db settings to settings file
        private readonly string Endpoint = "https://tvmaze.documents.azure.com:443/";
        private readonly string Key = "secret";
        private readonly string DatabaseId = "RvMazeShows";
        private readonly string CollectionId = "Items";
    }
}
