using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rtl.TVMaze.Domain.Model;
using Rtl.TVMaze.Service.Services;

namespace Rtl.TVMaze.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowsController : ControllerBase
    {
        public ShowsController(IStorageService<Show> documentDbService)
        {
            this.documentDbService = documentDbService;
        }

        [HttpGet]
        public async Task<IEnumerable<Show>> Get()
        {
            var shows = await documentDbService.GetItemsAsync(0);
            
            //var x = await documentDbService.GetItemAsync("5");
            return shows;
        }

        private readonly IStorageService<Show> documentDbService;
    }
}
