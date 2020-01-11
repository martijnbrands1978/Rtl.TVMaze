using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Rtl.TVMaze.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rtl.TVMaze.Service.Services
{
    public class DocumentDbService<T> : IStorageService<T> where T : class
    {
        public async Task<Document> CreateItemAsync(T item)
        {
            return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), item);
        }

        public async Task<IEnumerable<T>> GetItemsAsync(int page)
        {
            IDocumentQuery<Show> query = client.CreateDocumentQuery<Show>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .OrderBy(x => x.id).Skip(10).Take(10)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public DocumentDbService()
        {
            this.client = new DocumentClient(new Uri(Endpoint), Key);
        }

        private DocumentClient client;
        private readonly string Endpoint = "https://tvmaze.documents.azure.com:443/";
        private readonly string Key = "itlU4w70c1Z070V3yi06hj0eZCXd3OuTWR5CuBk53bjHQ8l6b3X672Pq1CtCssNB8WDhh400leGBDlJGr6xa0Q==";
        private readonly string DatabaseId = "RvMazeShows";
        private readonly string CollectionId = "Items";
    }
}
