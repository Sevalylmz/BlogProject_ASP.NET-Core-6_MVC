using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.WEB.Areas.Member.Models.DTOs
{
    public class AppUserUpdateDTO
    {
        public int ID { get; set; }

        public string IdentityID { get; set; }

        public string oldImage { get; set; }

        public string oldPassword { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "En az 3 karakter yazılmalıdır")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "En az 3 karakter yazılmalıdır")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "En az 3 karakter yazılmalıdır")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "En az 3 karakter yazılmalıdır")]
        [DataType(DataType.Password)] // UI işleminde password input şeklinde çıksın diye
        public string Password { get; set; }
        public string? Password1 { get; set; }
        public string? Password2 { get; set; }
        public string? Password3 { get; set; }


        public string? Image { get; set; } // Kullanıcıya gösterilmeyecek ama AppUser ın dosya yolunu tuttuğu propertydir.

        // Database tablosunda olmadığı için [NOTMAPPED] attribute ile işaretlenir. Otomatik MAP işlemi yapılacağı zaman hata vermemesi için yapılır. 
        // Kullanıcı güncelleme işlemi yaparken resmini güncellemek zorunda değildir.

        [NotMapped]
        public IFormFile? ImagePath { get; set; }

        // Identity için almayı tercih ettim

        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [DataType(DataType.EmailAddress)] // UI işleminde E-Mail input şeklinde çıksın diye
        public string Mail { get; set; }
    }
}
