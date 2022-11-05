using BlogProject.Models.Entities.Concrrete;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.WEB.Areas.Member.Models.VMs
{
    public class ArticleDetailVM
    {
        
        public Article? Article { get; set; }

        [Required(ErrorMessage = "boş yorum yapılamaz.")]
        [MinLength(5, ErrorMessage = "En az 5 karakter yazınız"), MaxLength(200)]
        public string CommentText { get; set; } // Kullanıcı yorum yazdığı zaman View üzerinden veriyi Comment/create a taşımak için kullanılıyor

        public int? ActiveAppUserID { get; set; }

        public int? ActiveArticleID { get; set; }

        public string? Mail { get; set; }

        public int? CommentID { get; set; }
    }
}
