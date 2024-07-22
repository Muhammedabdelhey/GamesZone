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
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{ImageSettings.ImagesPath}/games";
        }
        public async Task CreateAsync(CreateGameFormViewModel model)
        {
            var CoverName = await SaveImage(model.Cover);
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
            await _context.SaveChangesAsync();
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

        public async Task<Game?> UpdateAsync(EditGameViewModel model)
        {
            var game = await _context.Games
                .Include(x => x.GamePlatforms)
                .SingleOrDefaultAsync(p => p.Id == model.Id);
            if (game is null)
            {
                return null;
            }
            var oldCover = game.Cover;
            var hasNewCover = model.Cover != null;
            if (hasNewCover)
            {
                DeleteImage(oldCover);
                game.Cover = await SaveImage(model.Cover!);
            }
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.GamePlatforms = model.SelectdPlatforms
            .Select(platformId => new GamePlatform { PlatformId = platformId })
            .ToList();
            await _context.SaveChangesAsync();
            return game;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var game = await _context.Games
                .SingleOrDefaultAsync(p => p.Id == id);
            if (game is null)
            {
                return false;
            }
            _context.Remove(game);
            DeleteImage(game.Cover);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<string> SaveImage(IFormFile cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagesPath, CoverName);
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return CoverName;
        }

        public void DeleteImage(string cover)
        {
            var coverName = Path.Combine(_imagesPath, cover);
            File.Delete(coverName);
        }
    }
}
