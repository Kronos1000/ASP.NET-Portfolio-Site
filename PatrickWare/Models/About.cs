using System.ComponentModel.DataAnnotations.Schema;

namespace PatrickWare.Models
{
    public class About
    {

      public string? Id { get; set; }
        public string? Headings { get; set; }

        public string? Description { get; set; }

        public string? ImageFileName { get; set; }

        [NotMapped]
        public IFormFile? imageFile { get; set; }

        public bool? roundedBorder { get; set; }

        public int? ImageWidth { get; set; }
        public int? ImageHeight { get; set; }

    }
}
