namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        public GamesService(ApplicationDbContext context
            , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}/assests/images/games";
        }
        public async Task CreateAsync(CreateGameFormViewModel model)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            var path = Path.Combine(_imagesPath, CoverName);
            using var stream = File.Create(path);
            await model.Cover.CopyToAsync(stream);
            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = CoverName,
                GamePlatforms = model.SelectdPlatforms
                .Select(p => new GamePlatform { PlatformId = p })
                .ToList(),
            };
            _context.games.Add(game);
            _context.SaveChanges();
        }
    }
}
