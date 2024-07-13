namespace GameZone.Models
{
    public class Platform :BaseEntity
    {

        [MaxLength(500)]
        public string Icone { get; set; }=string.Empty;
        public virtual ICollection<Game> Games { get; set; }=new List<Game>();
    }
}
