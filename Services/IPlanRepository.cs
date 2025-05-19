using TimeIsMoney.Models;

namespace TimeIsMoney.Services;

internal interface IPlanRepository
{
    Task<bool> AddUpdatePlanAsync(Plan plan);

    Task<bool> DeletePlanAsync(int planId);

    Task<Plan> GetPlanAsync(int planId);

    Task<IEnumerable<Plan>> GetPlanAsync();
}