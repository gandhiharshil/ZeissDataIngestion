using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeissDataIngestion.Model;
using ZeissDataIngestion.Repository;

namespace ZeissDataIngestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtractorController : ControllerBase
    {

        private readonly ILogger<IngestionController> _logger;
        private readonly IMachineDataRepository _repository;
        public ExtractorController(IMachineDataRepository repository, ILogger<IngestionController> logger)
        {
            _logger = logger;
            _repository = repository;
      
        }
        [HttpGet]
        public async Task<ActionResult<List<MachineData>>> GetMachineData()
        {
            var machineData = await _repository.GetMachineData();

            return Ok(machineData);
        }
    }

   
}
