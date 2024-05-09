
using CsvHelper;
using ETL.API.BLL.Mapping;
using ETL.API.DAL.Models;
using ETL.API.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Globalization;

namespace ETL.API.BLL.Service
{
    public class ETLDataService : IETLDataService
    {
        private readonly IETLDataRepository _repository;

        public ETLDataService(IETLDataRepository repository)
        {
            _repository = repository;
        }

        public async Task ProcessCsvFileAsync(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<ETLDataMap>();
            var records = csv.GetRecords<ETLData>();

            foreach (var record in records)
            {
                record.StoreAndFwdFlag = record.StoreAndFwdFlag?.Trim();

                // Check if the record already exists in the database.
                var existingRecord = await _context.TripData
                    .FirstOrDefaultAsync(r => r.Id == record.Id); // Replace 'Id' with the appropriate unique identifier for your records.

                if (existingRecord != null)
                {
                    // If the record exists, update it.
                    _context.Entry(existingRecord).CurrentValues.SetValues(record);
                }
                else
                {
                    // If the record does not exist, add it.
                    _context.TripData.Add(record);
                }
            }

            // Save changes to the database.
            await _context.SaveChangesAsync();
        }
    }
}
