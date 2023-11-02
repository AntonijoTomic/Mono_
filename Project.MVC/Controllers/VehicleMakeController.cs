using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Project.MVC.ViewModels;
using Project.Service;
using Project.Service.Classes;
using Project.Service.Interfaces;
using Project.Service.Models;
using Project.Service.Services;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleService _vehicleMakeService;
        private readonly IPaginationService<VehicleMake> _pagination;
        private readonly IMapper _mapper;

        public VehicleMakeController(IMapper mapper)
        {
            _vehicleMakeService = Di.Create<IVehicleService>();
            _pagination = Di.Create<IPaginationService<VehicleMake>>();
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string sortby, string sortorder, string filter, int page = 1, int pageSize=5)
        {
            var vehicleMakes = await _vehicleMakeService.GetAllVehiclesMakes();

            if (!string.IsNullOrEmpty(sortby))
            {
                vehicleMakes = await _vehicleMakeService.SortMakes( sortby, sortorder);
            }
            if (!string.IsNullOrEmpty(filter))
            {
                vehicleMakes = await _vehicleMakeService.FilterBySearch((List<VehicleMake>)vehicleMakes, filter);
            }        
            var totalItems = vehicleMakes.Count();
            var totalPages = _pagination.GetTotalPages(totalItems, pageSize);
            var pagedMakes = _pagination.GetPage(vehicleMakes, page, pageSize);
           
            var paginationInfo = new PaginationInfo
            {
                TotalItems = totalItems,
                ItemsPerPage = pageSize,
                CurrentPage = page,
                TotalPages = totalPages
            };
            var makesViewModel = new VehicleMakeView
            {
                SortBy = sortby,
                SortOrder = sortorder,
                PaginationInfo = paginationInfo,
                Filter =filter,
                Makes = _mapper.Map<IEnumerable<VehicleMake>>(pagedMakes)
            };

            return View(makesViewModel);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(VehicleMake make)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<VehicleMake>(make);

                await _vehicleMakeService.CreateVehicleMake(entity);
                return RedirectToAction("Index");
            }
            return View(make);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            int result = await _vehicleMakeService.DeleteVehicleMakes(id);

            if (result == 1)
            {
                return RedirectToAction("Index");
            }
            else if (result == 0)
            {
                return NotFound();
            }
            else
            {
                return StatusCode(500, "Failed to delete vehicle make.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateVehicleMake(VehicleMake updatedVehicle)
        {
            int result = await _vehicleMakeService.UpdateVehicleMake(updatedVehicle);

            if (result == 1)
            {
                return RedirectToAction("Index");
            }
            else if (result == 0)
            {
                return NotFound("Vehicle make not found.");
            }
            else
            {
                return StatusCode(500, "Failed to update vehicle make.");
            }
        }
    }
}
