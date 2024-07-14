using GameZone.Services;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryService _categoryService;
        public GamesController(ApplicationDbContext context,ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            CraeteGameFormViewModel viewModel = new()
            {
                Categories = await _categoryService.GetCategoriesForDropDown(),
                Platforms = await _context.platforms
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .ToListAsync()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CraeteGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _categoryService.GetCategoriesForDropDown();
                model.Platforms = await _context.platforms
                .Select(c => new SelectListItem { Text = c.Id.ToString(), Value = c.Name })
                .OrderBy(c => c.Text)
                .ToListAsync();
                return View(model);
            }
            return View();
        }
    }
}
