using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatrickWare.Models
{
    public class PreviousExperience
    {
        [Key]
        public int ProjectID { get; set; } 
        
        public string? ProjectTitle { get; set; }

        public string? ShortProjectDescription { get; set; }

        public string? ProjectDescription { get; set; }

        public string? ProjectPurpose { get; set; }




        public string? ProjectImageFileName { get; set; }

        [NotMapped]
        public IFormFile? ProjectImageFile { get; set; }
        public string? DevLanguages { get; set; }
        public string? GitRepoLink { get; set; }
        public bool? roundedBorder { get; set; }

        public int? ImageWidth { get; set; }
        public int? ImageHeight { get; set; }

    }
}
