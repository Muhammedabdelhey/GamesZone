namespace GameZone.ViewModels
{
    public class EditGameViewModel : GameViewModel
    {
        public int Id { get; set; }
        public string? CoverName { get; set; }

        [AllowedExtensions, MaxFileSize]
        public IFormFile? Cover { get; set; } = default!;
    }
}
