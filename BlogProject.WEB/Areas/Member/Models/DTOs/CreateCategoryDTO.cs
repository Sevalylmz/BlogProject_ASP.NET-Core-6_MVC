using System.ComponentModel.DataAnnotations;

namespace BlogProject.WEB.Areas.Member.Models.DTOs
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage ="bu alan boş bırakılamaz")] //not null
        [MinLength(2),MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "bu alan boş bırakılamaz")] //not null
        [MinLength(2), MaxLength(250)]
        public string Description { get; set; }
    }
}
