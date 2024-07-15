namespace GameZone.Models
{
    [Table("gamesplatforms")]
    public class GamePlatform
    {
        public int GameId {  get; set; }
        public int PlatformId {  get; set; }
    }
}
