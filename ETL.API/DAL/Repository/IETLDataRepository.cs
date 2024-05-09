using ETL.API.DAL.Models;

namespace ETL.API.DAL.Repository
{
    public interface IETLDataRepository
    {
        Task AddEtlDataAsync(ETLData etlData);
    }
}
