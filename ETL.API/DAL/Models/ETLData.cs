using System.ComponentModel.DataAnnotations;

namespace ETL.API.DAL.Models
{
    public class ETLData
    {
        [Key]
        public int Id { get; set; }
        public DateTime TpepPickupDatetime { get; set; }
        public DateTime TpepDropoffDatetime { get; set; }
        public int? PassengerCount { get; set; }
        public float TripDistance { get; set; }
        public string? StoreAndFwdFlag { get; set; }
        public int PULocationID { get; set; }
        public int DOLocationID { get; set; }
        public float FareAmount { get; set; }
        public float TipAmount { get; set; }
    }
}
