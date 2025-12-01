using Microsoft.AspNetCore.Mvc;
using ServiceRequestTracker.Models;
using ServiceRequestTracker.Repositories;

namespace ServiceRequestTracker.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly IServiceRequestRepository _repository;

        public ServiceRequestsController(IServiceRequestRepository repository)
        {
            _repository = repository;
        }

        // GET: ServiceRequests
        public async Task<IActionResult> Index(string statusFilter, string departmentFilter, string searchText)
        {
            ViewBag.StatusFilter = statusFilter;
            ViewBag.DepartmentFilter = departmentFilter;
            ViewBag.SearchText = searchText;

            var requests = await _repository.GetAllAsync(statusFilter, departmentFilter, searchText);
            return View(requests);
        }

        // GET: ServiceRequests/Create
        public IActionResult Create()
        {
            return View(new ServiceRequest());
        }

        // POST: ServiceRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                var newId = await _repository.CreateAsync(request);
                TempData["SuccessMessage"] = $"Request created successfully. Reference ID: {newId}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving the request. Please try again.");
                return View(request);
            }
        }

        // GET: ServiceRequests/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var req = await _repository.GetByIdAsync(id);
            if (req == null) return NotFound();

            return View(req);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceRequest request)
        {
            if (id != request.RequestID)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(request);

            try
            {
                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                    return NotFound();
                existing.Title = request.Title;
                existing.Description = request.Description;
                existing.RequesterName = request.RequesterName;
                existing.Department = request.Department;
                existing.Priority = request.Priority;
                existing.Status = request.Status;
                existing.LastUpdatedOn = DateTime.Now;
                await _repository.UpdateAsync(existing);

                TempData["SuccessMessage"] = "Request updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the request.");
                return View(request);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var req = await _repository.GetByIdAsync(id);
            if (req == null)
                return NotFound();

            return View(req);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
        public async Task<IActionResult> ExportPdf()
        {
            var data = await _repository.GetAllAsync(null, null, null);

            return new Rotativa.AspNetCore.ViewAsPdf("ExportPdf", data)
            {
                FileName = "ServiceRequests.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }

    }
}