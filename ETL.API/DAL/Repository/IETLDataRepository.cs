using ETL.API.DAL.Models;

namespace ETL.API.DAL.Repository
{
    public interface IETLDataRepository
    {
        Task AddETLDataAsync(List<ETLData> etlData);
        Task<object> GetLocationWithHighestAverageTipAsync();
        Task<List<ETLData>> GetTop100LongestFaresAsync();
        Task<List<ETLData>> GetTop100LongestFaresByTimeAsync();
        Task<List<ETLData>> SearchByPULocationIdAsync(int puLocationId);
        Task<List<ETLData>> IdentifyAndRemoveDuplicatesAsync();
    }
}
