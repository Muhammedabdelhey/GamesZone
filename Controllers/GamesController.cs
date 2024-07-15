using GameZone.Services;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly IPlatfromsService _platformService;
        private readonly ICategoriesService _categoryService;
        private readonly IGamesService _gamesService;
        public GamesController(IPlatfromsService context,
            ICategoriesService categoryService,
            IGamesService gamesService)
        {
            _platformService = context;
            _categoryService = categoryService;
            _gamesService = gamesService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            CreateGameFormViewModel viewModel = new()
            {
                Categories = await _categoryService.GetCategoriesForDropDownAsync(),
                Platforms = await _platformService.GetPlatformsForDropDownAsync(),
            };
            return View(viewModel);
        }
        [HttpPost]
        // CSRF Token this attr valid token that pass on form
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _categoryService.GetCategoriesForDropDownAsync();
                model.Platforms = await _platformService.GetPlatformsForDropDownAsync();
                return View(model);
            }
            await _gamesService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
