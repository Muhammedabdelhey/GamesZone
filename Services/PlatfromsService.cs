
namespace GameZone.Services
{
    public class PlatfromsService : IPlatfromsService
    {
        private readonly ApplicationDbContext _context;
        public PlatfromsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SelectListItem>> GetPlatformsForDropDownAsync()
        {
            return await _context.platforms
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
