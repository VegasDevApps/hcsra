using DomainModule.Entities;

namespace DomainModule.Interfaces
{
    public interface IInsurancePolicyRepository
    {
        Task<IReadOnlyList<InsurancePolicy>>  GetInsurancePoliciesByUserId(int id);
        Task<InsurancePolicy>  GetInsurancePolicyById(int id);
        Task<bool> AddInsurancePolicy(InsurancePolicy insurancePolicy);
        Task<bool> UpdateInsurancePolicy(InsurancePolicy insurancePolicy);
        Task<bool> DeleteInsurancePolicyById(int id);
    }
}