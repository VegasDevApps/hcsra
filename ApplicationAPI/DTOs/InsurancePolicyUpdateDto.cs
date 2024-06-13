namespace ApplicationAPI.DTOs
{
    public class InsurancePolicyUpdateDto
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public int InsuranceAmount { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}