using ETL.API.DAL.Models;

namespace ETL.API.BLL.Service
{
    public interface IETLDataService
    {
        Task ProcessCsvFileAsync(IFormFile file);
        Task<object> GetLocationWithHighestAverageTipAsync();
        Task<List<ETLData>> GetTop100LongestFaresAsync();
        Task<List<ETLData>> GetTop100LongestFaresByTimeAsync();
        Task<List<ETLData>> SearchByPULocationIdAsync(int puLocationId);
        Task<List<ETLData>> HandleDuplicatesAsync();
    }
}
