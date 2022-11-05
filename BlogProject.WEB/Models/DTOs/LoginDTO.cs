using System.ComponentModel.DataAnnotations;

namespace BlogProject.WEB.Models.DTOs
{
    public class LoginDTO
    {
        // Kullanıcının giriş işlemi için vereceği olan 2 adet parametrenin View dan controller a taşınması için 2 propertyli sınıf oluşturuldu. 
        // Bu propertyler boş bırakılamaz
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
