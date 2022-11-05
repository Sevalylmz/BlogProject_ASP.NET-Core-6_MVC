using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.WEB.Areas.Member.Models.DTOs
{
    public class ArticleUpdateDTO
    {
        //update te ıd ye ihtiyacım var
        public int ID { get; set; }

        [Required(ErrorMessage = "bu alan boş bırakılamaz")]
        [MinLength(2), MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "bu alan boş bırakılamaz")]
        [MinLength(2), MaxLength(600)]
        public string Content { get; set; }
        public string? Image { get; set; }

        //[Required] NOT => MAKALE güncellenirken foto değişmeyedebilir diye required kaldırıldı
        [NotMapped]
        public IFormFile ImagePath { get; set; }

        [Required] //kategorisiz makale olmaz
        public int CategoryID { get; set; }

        [Required]
        public int AppUserID { get; set; }
        public List<GetCategoryDTO>? Categories { get; set; } //kategorinin bazı proplarını göndereceğim Lİst<Category> değil bu yüden. Aslında dto içine dto gömülmüş oluyor
    }
}
