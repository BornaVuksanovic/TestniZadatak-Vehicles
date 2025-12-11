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

        public async Task<IActionResult> Index()
        {
            var makes = await _vehicleService.GetVehicleMakesAsync();
            var model = _mapper.Map<List<VehicleMakeViewModel>>(makes);
            return View(model);
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
