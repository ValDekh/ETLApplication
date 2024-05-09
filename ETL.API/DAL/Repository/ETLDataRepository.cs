using ETL.API.DAL.Models;

namespace ETL.API.DAL.Repository
{
    public class ETLDataRepository : IETLDataRepository
    {
        private readonly AppDbContext _context;

        public ETLDataRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddEtlDataAsync(ETLData etlData)
        {
            _context.TripData.Add(etlData);
            await _context.SaveChangesAsync();
        }
    }
}
