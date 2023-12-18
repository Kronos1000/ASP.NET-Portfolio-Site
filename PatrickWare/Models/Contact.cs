using System.ComponentModel.DataAnnotations;

namespace PatrickWare.Models
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}


