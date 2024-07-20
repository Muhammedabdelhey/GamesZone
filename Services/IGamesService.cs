namespace GameZone.Services
{
    public interface IGamesService
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game?>GetByIdAsync(int id);
        Task CreateAsync(CreateGameFormViewModel model);
        Task<Game?> UpdateAsync(EditGameViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}
