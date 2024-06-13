namespace ApplicationAPI.DTOs
{
    public class InsurancePolicyAddDto
    {
        public string PolicyNumber { get; set; }
        public int InsuranceAmount { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int UserId { get; set; }
    }
}