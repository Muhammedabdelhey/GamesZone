﻿namespace GameZone.ViewModels
{
    public class GameViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Platforms { get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name = "Supported Platforms")]
        public List<int> SelectdPlatforms { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
    }
}
