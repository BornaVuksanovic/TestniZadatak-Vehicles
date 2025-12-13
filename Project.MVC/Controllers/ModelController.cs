using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Data.Entities;
using Project.MVC.ViewModels;
using Project.Service.Interfaces;

namespace Project.MVC.Controllers
{
    public class ModelController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleService _service;

        public ModelController(IVehicleService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task<ActionResult> Index(int? makeId, string? searchingString, string? sortOrder, int pageNumber = 1, int pageSize = 3)
        {
            var (items, totalCount) = await _service.GetModelsAsync(makeId, searchingString, sortOrder, pageNumber, pageSize);

            var makes = await _service.GetVehicleMakesAsync();

            var vm = new ModelIndexViewModel
            {
                MakeId = makeId,
                SearchingString = searchingString,
                CurrentSort = sortOrder,

                NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "",
                AbrvSortParm = sortOrder == "abrv" ? "abrv_desc" : "abrv",
                MakeSortParm = sortOrder == "make" ? "make_desc" : "make",

                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,

                Makes = makes.Select(m => new MakeDropdownItem { Id = m.Id, Name = m.Name }).ToList(),

                Items = _mapper.Map<IEnumerable<VehicleModelViewModel>>(items)
            }; 

            return View(vm);

        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Makes = (await _service.GetVehicleMakesAsync())
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleModelViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Makes = (await _service.GetVehicleMakesAsync())
                    .Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.Name
                    });

                return View(vm);
            }

            var entity = _mapper.Map<VehicleModel>(vm);
            await _service.AddModelAsync(entity);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _service.GetModelByIdAsync(id);
            if (model == null) return NotFound();

            ViewBag.Makes = (await _service.GetVehicleMakesAsync())
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                });

            return View(_mapper.Map<VehicleModelViewModel>(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleModelViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await _service.UpdateModelAsync(
                _mapper.Map<VehicleModel>(vm));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _service.GetModelByIdAsync(id);
            if (model == null) return NotFound();

            return View(_mapper.Map<VehicleModelViewModel>(model));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteModelAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
