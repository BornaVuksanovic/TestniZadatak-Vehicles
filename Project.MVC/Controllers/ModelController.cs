using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

                Makes = makes.Select(m => new MakeDropdownItem { Id = m.Id, Name = m.Name }).ToList()

                Items = _mapper.Map<IEnumerable<ModelIndexViewModel>>(items)
            };

            return View(vm);

        }


    }
}
