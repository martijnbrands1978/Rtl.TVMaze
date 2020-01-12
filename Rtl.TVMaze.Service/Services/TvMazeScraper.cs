using AutoMapper;
using RestSharp;
using Rtl.TVMaze.Domain.Model;
using Rtl.TVMaze.Service.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rtl.TVMaze.Service.Services
{
    public class TvMazeScraper : IMetaDataScraper
    {
        /// <summary>
        /// Enrich shows with cast. Cast is sorted by birthady descending.
        /// </summary>
        /// <param name="shows"></param>
        public void EnrichShows(IEnumerable<Show> shows)
        {
            foreach(var show in shows)
            {
                RestRequest request = new RestRequest($"shows/{show.id}/cast", DataFormat.Json);
                IRestResponse response = GetRequest(request);
                List<TvMazeCastMemberDto> cast = JsonSerializer.Deserialize<List<TvMazeCastMemberDto>>(response.Content);

                show.Cast = mapper.Map<IEnumerable<CastMember>>(cast.OrderByDescending(c => c.Person.BirthDay));
            }
        }

        /// <summary>
        /// Get shows by page, each page return aprox 250 shows
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public IEnumerable<Show> GetShows(int page)
        {
            RestRequest request = new RestRequest($"shows?page={page}", DataFormat.Json);
            IRestResponse response = client.Get(request);
            List<TvMazeShowDto> shows = JsonSerializer.Deserialize<List<TvMazeShowDto>>(response.Content);

            return mapper.Map<List<Show>>(shows);
        }

        public TvMazeScraper(IMapper mapper)
        {
            this.mapper = mapper;
            this.client = new RestClient("http://api.tvmaze.com");
        }

        private IRestResponse GetRequest(RestRequest request)
        {
            IRestResponse response = null;
            for (int i = 0; i < 5; i++)
            {
                response = client.Get(request);

                if (response.IsSuccessful)
                    return response;
                else if(response.StatusCode == HttpStatusCode.TooManyRequests)
                    Task.Delay(5000).Wait();
            }

            throw response.ErrorException;
        }

        private readonly IMapper mapper;
        private readonly RestClient client;
    }
}
