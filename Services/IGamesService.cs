namespace GameZone.Services
{
    public interface IGamesService
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task CreateAsync(CreateGameFormViewModel model);
    }
}
