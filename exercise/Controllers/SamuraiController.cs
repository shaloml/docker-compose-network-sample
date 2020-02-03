using ex4.Dal.Model;
using Infrastructure.DataAcsess.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ex4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SamuraiController : ControllerBase
    {
        private readonly ILogger<SamuraiController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Samurai> _repo;

        public SamuraiController(ILogger<SamuraiController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this._unitOfWork = unitOfWork;
            _repo = _unitOfWork.CreateRepository<Samurai>();
        }

        [HttpGet]
        public async Task<IEnumerable<Samurai>> Get()
        {
            return await _repo.FindAllAsync();
        }

        [HttpPost]
        public async Task Post(Samurai samurai)
        {
            _repo.Insert(samurai);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
