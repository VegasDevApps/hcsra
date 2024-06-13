using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModule.Entities
{
    [Table("InsurancePolicies")]
    public class InsurancePolicy
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public int InsuranceAmount { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        
        public User User { get; set; }
        public int UserId { get; set; }
    }
}