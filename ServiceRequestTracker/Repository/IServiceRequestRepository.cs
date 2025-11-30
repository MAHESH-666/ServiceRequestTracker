using ServiceRequestTracker.Models;

namespace ServiceRequestTracker.Repositories
{
    public interface IServiceRequestRepository
    {
        Task<List<ServiceRequest>> GetAllAsync(string? statusFilter, string? departmentFilter, string? searchText);
        Task<ServiceRequest?> GetByIdAsync(int id);
        Task<int> CreateAsync(ServiceRequest request);
        Task UpdateAsync(ServiceRequest request);
        Task DeleteAsync(int id);

    }
}
