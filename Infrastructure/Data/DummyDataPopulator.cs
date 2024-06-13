using System.Text.Json;
using DomainModule.Entities;

namespace Infrastructure.Data
{
    public class DummyDataPopulator
    {
        public static async Task Populate(AppDbContext appDbContext)
        {
            if(!appDbContext.Users.Any())
            {
                var usersData = File.ReadAllText("../Infrastructure/Data/DummyData/users.json");
                var users = JsonSerializer.Deserialize<List<User>>(usersData);
                appDbContext.Users.AddRange(users);
            }

            if(!appDbContext.InsurancePolicies.Any())
            {
                var insurancePoliciesData = File.ReadAllText("../Infrastructure/Data/DummyData/policies.json");
                var insurancePolicies = JsonSerializer.Deserialize<List<InsurancePolicy>>(insurancePoliciesData);
                appDbContext.InsurancePolicies.AddRange(insurancePolicies);
            }

            if(appDbContext.ChangeTracker.HasChanges()) 
                await appDbContext.SaveChangesAsync();
        }
    }
}