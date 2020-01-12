using Microsoft.AspNetCore.Mvc;
using Rtl.TVMaze.Service.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Rtl.TVMaze.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowsController : ControllerBase
    {
        public ShowsController(IStorageService documentDbService)
        {
            this.documentDbService = documentDbService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? page)
        {
            page = page ?? 0;
            var shows = await documentDbService.GetItemsAsync((int)page);

            if (!shows.Any())
            {
                return NotFound($"page {page} not found");
            }

            return Ok(shows);
        }

        private readonly IStorageService documentDbService;
    }
}
