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
        public async Task<IActionResult> ProcessCsvFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            await _service.ProcessCsvFileAsync(file);
            return Ok();
        }

            [HttpGet("location-with-highest-average-tip")]
        public async Task<IActionResult> GetLocationWithHighestAverageTip()
        {
            var result = await _service.GetLocationWithHighestAverageTipAsync();
            return Ok(result);
        }

        [HttpGet("top-100-longest-fares")]
        public async Task<IActionResult> GetTop100LongestFares()
        {
            var result = await _service.GetTop100LongestFaresAsync();
            return Ok(result);
        }

        [HttpGet("top-100-longest-fares-by-time")]
        public async Task<IActionResult> GetTop100LongestFaresByTime()
        {
            var result = await _service.GetTop100LongestFaresByTimeAsync();
            return Ok(result);
        }

        [HttpGet("search-by-pulocationid/{puLocationId}")]
        public async Task<IActionResult> SearchByPULocationId(int puLocationId)
        {
            var result = await _service.SearchByPULocationIdAsync(puLocationId);
            return Ok(result);
        }

        [HttpDelete("duplicates")]
        public async Task<IActionResult> HandleDuplicatesAsync()
        {
            var duplicates = await _service.HandleDuplicatesAsync();
            return Ok(duplicates);
        }
    }

}
