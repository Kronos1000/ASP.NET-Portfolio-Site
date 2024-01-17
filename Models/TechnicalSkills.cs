using System.ComponentModel.DataAnnotations;
namespace PatrickWare.Models
{
    public class TechnicalSkills
    {

        [Key]
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int SkillAbilityPercentage { get; set; }
    }
}
