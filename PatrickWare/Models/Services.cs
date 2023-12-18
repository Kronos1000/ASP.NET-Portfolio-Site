using System.ComponentModel.DataAnnotations;
namespace PatrickWare.Models
{
    public class Services
    {
        [Key]
        public int ServiceID { get; set; }
        public string? BackgroundImage { get; set; }
        public string? ServicesTitle { get; set; }
        public string? ServicesDescription { get; set; }
        public string? Icon1 { get; set; }
        public string? Icon2 { get; set; }
        public string? Icon3 { get; set; }
    }
}
