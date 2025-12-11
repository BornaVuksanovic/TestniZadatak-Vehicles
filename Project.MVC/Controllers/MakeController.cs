using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.MVC.ViewModels;
using Project.Service.Interfaces;


namespace Project.MVC.Controllers
{
    public class MakeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleService _vehicleService;

        public MakeController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string? sortOrder, string? currentFilter, string? searchingString, int pageNumber=1)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = sortOrder == "abrv" ? "abrv_desc" : "abrv";

            if (searchingString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchingString = currentFilter;
            }

            const int pageSize = 3;

            var result = await _vehicleService.GetMakesAsync(searchingString, sortOrder, pageNumber, pageSize);

            var vm = new MakeIndexViewModel
            {
                Makes = _mapper.Map<IEnumerable<VehicleMakeViewModel>>(result.Items),
                CurrentFilter = searchingString,
                CurrentSort = sortOrder,
                PageNumber = pageNumber,
                TotalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize),
            };

            return View(vm);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleMakeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var entity = _mapper.Map<Project.Data.Entities.VehicleMake>(vm);
            await _vehicleService.AddMakeAsync(entity);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int id)
        {
            var make = await _vehicleService.GetMakeByIdAsync(id);
            if (make == null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<VehicleMakeViewModel>(make);
            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleMakeViewModel vm) 
        {
            if (id != vm.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(vm);

            var entity = _mapper.Map<Project.Data.Entities.VehicleMake>(vm);
            await _vehicleService.UpdateMakeAsync(entity);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var make = await _vehicleService.GetMakeByIdAsync(id);
            if(make == null)
                return NotFound();

            var vm = _mapper.Map<VehicleMakeViewModel>(make);
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var make = await _vehicleService.GetMakeByIdAsync(id);
            if (make == null)
                return NotFound();

            await _vehicleService.DeleteMakeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
