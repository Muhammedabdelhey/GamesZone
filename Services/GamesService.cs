using GameZone.Settings;

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
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{ImageSettings.ImagesPath}";
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
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games
                .Include(x => x.Category)
                .Include(x => x.Platforms)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games
                .Include(x => x.Category)
                .Include(x => x.Platforms)
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == id);
        }
    }
}
