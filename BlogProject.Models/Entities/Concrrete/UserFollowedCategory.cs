namespace BlogProject.Models.Entities.Concrrete
{
    public class UserFollowedCategory // BaseEntity classından kalıtım almamaktadır. Çünkü ara tablo olduğu için CRUD operasyonlarının tümü geçerli değildir. ID almadığı için AppUserID ve CategoryID composite key olacaktır. Yani tekrarlar yapılamayacaktır. Ayrıca bu tablo sadece diğer tablolardan aldığı Foreing Key lerden ile oluştuğu için yani kendine has bir kolonu olmadığı için Primary Key kolonuna ihtiyaç duymaz.
    {
        // Navigation Property
   
        public int AppUserID { get; set; }
        public virtual AppUser AppUser { get; set; }

 
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}