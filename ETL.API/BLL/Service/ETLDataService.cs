
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
            var records = csv.GetRecords<ETLData>()
                .Select(p =>
                {
                    if (string.IsNullOrEmpty(p.StoreAndFwdFlag))
                    {
                        p.StoreAndFwdFlag = null;
                    }
                    p.StoreAndFwdFlag = p.StoreAndFwdFlag?.Trim();

                    // Convert 'N' to 'No' and 'Y' to 'Yes'
                    if (p.StoreAndFwdFlag == "N")
                    {
                        p.StoreAndFwdFlag = "No";
                    }
                    else if (p.StoreAndFwdFlag == "Y")
                    {
                        p.StoreAndFwdFlag = "Yes";
                    }

                    // Convert from EST to UTC
                    p.TpepPickupDatetime = TimeZoneInfo.ConvertTimeToUtc(p.TpepPickupDatetime, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
                    p.TpepDropoffDatetime = TimeZoneInfo.ConvertTimeToUtc(p.TpepDropoffDatetime, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

                    return p;
                })
                .ToList();
            await _repository.AddETLDataAsync(records);
        }

        public async Task<object> GetLocationWithHighestAverageTipAsync()
        {
            return await _repository.GetLocationWithHighestAverageTipAsync();
        }

        public async Task<List<ETLData>> GetTop100LongestFaresAsync()
        {
            return await _repository.GetTop100LongestFaresAsync();
        }

        public async Task<List<ETLData>> GetTop100LongestFaresByTimeAsync()
        {
            return await _repository.GetTop100LongestFaresByTimeAsync();
        }

        public async Task<List<ETLData>> SearchByPULocationIdAsync(int puLocationId)
        {
            return await _repository.SearchByPULocationIdAsync(puLocationId);
        }

        public async Task<List<ETLData>> HandleDuplicatesAsync()
        {
            var duplicates = await _repository.IdentifyAndRemoveDuplicatesAsync();

            using var writer = new StreamWriter("duplicates.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await csv.WriteRecordsAsync(duplicates);

            return duplicates;
        }
    }
}
