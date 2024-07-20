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
        public async Task<IActionResult> Index()
        {
            var games = await _gamesService.GetAllAsync();
            return View(games);
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

        [HttpGet]
        public async Task<IActionResult> Show(int id)
        {
            var game = await _gamesService.GetByIdAsync(id);
            if (game is null)
            {
                return NotFound();
            }
            return View(game);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await _gamesService.GetByIdAsync(id);
            if (game is null)
            {
                return NotFound();
            }
            EditGameViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                CoverName = game.Cover,
                SelectdPlatforms = game.Platforms.Select(p => p.Id).ToList(),
                Categories = await _categoryService.GetCategoriesForDropDownAsync(),
                Platforms = await _platformService.GetPlatformsForDropDownAsync(),
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _categoryService.GetCategoriesForDropDownAsync();
                model.Platforms = await _platformService.GetPlatformsForDropDownAsync();
                return View(model);
            };
            var game = await _gamesService.UpdateAsync(model);
            if (game is null)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _gamesService.DeleteAsync(id);
            return isDeleted ? Ok() : BadRequest();
        }
    }
}
