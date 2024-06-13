namespace ApplicationAPI.DTOs
{
    public class InsurancePolicyRequestDto
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public int InsuranceAmount { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int UserId { get; set; }
    }
}