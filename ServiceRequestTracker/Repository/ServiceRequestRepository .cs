using Microsoft.EntityFrameworkCore;
using ServiceRequestTracker.Models;

namespace ServiceRequestTracker.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly ServiceTrackerContext _context;

        public ServiceRequestRepository(ServiceTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceRequest>> GetAllAsync(string? statusFilter, string? departmentFilter, string? searchText)
        {
            var query = _context.ServiceRequests.AsQueryable();

            if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "All")
            {
                query = query.Where(r => r.Status == statusFilter);
            }

            if (!string.IsNullOrEmpty(departmentFilter) && departmentFilter != "All")
            {
                query = query.Where(r => r.Department == departmentFilter);
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(r =>
                    r.Title.Contains(searchText) ||
                    r.Description.Contains(searchText));
            }

            return await query
                .OrderByDescending(r => r.CreatedOn)
                .ToListAsync();
        }

        public async Task<ServiceRequest?> GetByIdAsync(int id)
        {
            return await _context.ServiceRequests.FindAsync(id);
        }

        public async Task<int> CreateAsync(ServiceRequest request)
        {
            request.Status = "New";
            request.CreatedOn = DateTime.Now;
            request.LastUpdatedOn = DateTime.Now;

            _context.ServiceRequests.Add(request);
            await _context.SaveChangesAsync();

            return request.RequestID;
        }

        public async Task UpdateAsync(ServiceRequest request)
        {
            request.LastUpdatedOn = DateTime.Now;
            _context.ServiceRequests.Update(request);
            await _context.SaveChangesAsync();
        }
    }
}
