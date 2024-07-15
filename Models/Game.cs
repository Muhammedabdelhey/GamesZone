namespace GameZone.Models
{
    public class Game : BaseEntity
    {

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Cover { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = default!;
        public virtual ICollection<Platform> Platforms { get; set; } = new List<Platform>();
        public virtual ICollection<GamePlatform> GamePlatforms { get; set; } = new List<GamePlatform>();
    }
}
