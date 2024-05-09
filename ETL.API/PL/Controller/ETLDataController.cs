using ETL.API.BLL.Service;
using Microsoft.AspNetCore.Mvc;

namespace ETL.API.PL.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ETLDataController : ControllerBase
    {
        private readonly IETLDataService _service;

        public ETLDataController(IETLDataService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessCsvFile([FromBody] string filePath)
        {
            await _service.ProcessCsvFileAsync(filePath);
            return Ok();

        }
    }
}
