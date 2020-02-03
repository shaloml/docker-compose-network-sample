using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using webApi.Model;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SamuraiController : ControllerBase
    {

        private readonly ILogger<SamuraiController> _logger;
        private readonly WebClientHandler _webClientHandler;

        public SamuraiController(ILogger<SamuraiController> logger, WebClientHandler webClientHandler)
        {
            _logger = logger;
            _webClientHandler = webClientHandler;
        }

        [HttpGet]
        public async Task<IEnumerable<Samurai>> Get()
        {
            return await _webClientHandler.GetAsync<List<Samurai>>("Samurai");
        }

        [HttpPost]
        public async Task Post(Samurai samurai)
        {
            await _webClientHandler.POSTAsync<Samurai>("Samurai", samurai);
        }
    }
}
