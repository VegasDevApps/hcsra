using Microsoft.EntityFrameworkCore;
using DomainModule.Entities;
using DomainModule.Interfaces;

namespace Infrastructure.Repositories
{
    public class InsurancePolicyRepository : IInsurancePolicyRepository
    {
        private readonly AppDbContext _dbContext;
        public InsurancePolicyRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            await _dbContext.InsurancePolicies.AddAsync(insurancePolicy);
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteInsurancePolicyById(int id)
        {
            InsurancePolicy policyToDelete = await GetInsurancePolicyById(id);
            if(policyToDelete != null)
            {
                _dbContext.InsurancePolicies.Remove(policyToDelete);
                return (await _dbContext.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public async Task<IReadOnlyList<InsurancePolicy>> GetInsurancePolicies()
        {
            return await _dbContext.InsurancePolicies.ToListAsync();
        }

        public async Task<InsurancePolicy> GetInsurancePolicyById(int id)
        {
            return await _dbContext.InsurancePolicies.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<InsurancePolicy>> GetInsurancePoliciesByUserId(int userId)
        {
            return await _dbContext.InsurancePolicies.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<bool> UpdateInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            var policyForUpdate = await GetInsurancePolicyById(insurancePolicy.Id);
            if(policyForUpdate != null)
            {

                policyForUpdate.PolicyNumber = insurancePolicy.PolicyNumber;
                policyForUpdate.InsuranceAmount = insurancePolicy.InsuranceAmount;
                policyForUpdate.StartDate = insurancePolicy.StartDate;
                policyForUpdate.EndDate = insurancePolicy.EndDate;

                _dbContext.InsurancePolicies.Update(policyForUpdate);
                return (await _dbContext.SaveChangesAsync()) > 0;
            }
            return false;
        }
    }
}