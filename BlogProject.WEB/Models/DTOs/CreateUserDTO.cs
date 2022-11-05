using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.WEB.Models.DTOs
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage ="Bu alan boş bırakılamaz")]
        [MinLength(3,ErrorMessage ="En az 3 karakter yazılmalıdır")]
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
               
        public string? Image { get; set; } // Kullanıcıya gösterilmeyecek ama AppUser ın dosya yolunu tuttuğu propertydir.BUNU KAYDEDERKEN KULLANIYORUM,ÖNCE IFORMFİLE KULLANILIYOR

        // Database tablosunda olmadığı için [NOTMAPPED] attribute ile işaretlenir. Otomatik MAP işlemi yapılacağı zaman hata vermemesi için yapılır. 
        // Kullanıcı profil resmi seçmeden kayıt işlemi olmamalıdır. Yani Image zorunludur.

        [Required] //DİKKAT => IDENTITY TARAFINDA OLUŞUP APPUSER TARAFINDA OLUŞMASINI BAŞTAN ENGELLEMEK İÇİN
        [NotMapped]
        public IFormFile ImagePath { get; set; } //BUNU FOTOĞRAFI YÜKLERKEN KULLANIYORUM

        // Identity için almayı tercih ettim

        [Required(ErrorMessage = "Bu alan boş bırakılamaz")] 
        [DataType(DataType.EmailAddress)] // UI işleminde E-Mail input şeklinde çıksın diye
        public string Mail { get; set; }
    }
}
