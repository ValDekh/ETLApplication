namespace ETL.API.BLL.Service
{
    public interface IETLDataService
    {
        Task ProcessCsvFileAsync(string filePath);
    }
}
