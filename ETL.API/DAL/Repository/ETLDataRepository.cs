using ETL.API.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ETL.API.DAL.Repository
{
    public class ETLDataRepository : IETLDataRepository
    {
        private readonly AppDbContext _context;

        public ETLDataRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddETLDataAsync(List<ETLData> etlData)
        {
            _context.ETLData.AddRange(etlData);
            await _context.SaveChangesAsync();
        }

        public async Task<object> GetLocationWithHighestAverageTipAsync()
        {
            var data = await _context.ETLData
               .GroupBy(t => t.PULocationID)
               .Select(g => new { PULocationID = g.Key, AverageTipAmount = g.Average(t => t.TipAmount) })
               .OrderByDescending(g => g.AverageTipAmount)
               .FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<ETLData>> GetTop100LongestFaresAsync()
        {
            return await _context.ETLData
                .OrderByDescending(t => t.TripDistance)
                .Take(100)
                .ToListAsync();
        }

        public async Task<List<ETLData>> GetTop100LongestFaresByTimeAsync()
        {
            return await _context.ETLData
                .OrderByDescending(t => EF.Functions.DateDiffSecond(t.TpepDropoffDatetime, t.TpepPickupDatetime))
                .Take(100)
                .ToListAsync();
        }

        public async Task<List<ETLData>> SearchByPULocationIdAsync(int puLocationId)
        {
            return await _context.ETLData
                .Where(t => t.PULocationID == puLocationId)
                .ToListAsync();
        }

        public async Task<List<ETLData>> IdentifyAndRemoveDuplicatesAsync()
        {
            var duplicates = _context.ETLData
                .Where(t => _context.ETLData
                    .Count(t2 => t2.TpepPickupDatetime == t.TpepPickupDatetime &&
                                 t2.TpepDropoffDatetime == t.TpepDropoffDatetime &&
                                 t2.PassengerCount == t.PassengerCount) > 1)
                .ToList();

            _context.ETLData.RemoveRange(duplicates);
            await _context.SaveChangesAsync();

            return duplicates;
        }
    }
}
