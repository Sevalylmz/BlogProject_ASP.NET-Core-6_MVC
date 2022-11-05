namespace BlogProject.Models.Entities.Concrrete
{

    public class Like // BaseEntity classından kalıtım almamaktadır. Çünkü beğeni için CRUD operasyonlarının tümü geçerli değildir. ID almadığı için AppUserID ve ArticleID composite key olacaktır. Yani tekrarlar yapılamayacaktır. Ayrıca bu tablo sadece diğer tablolardan aldığı Foreing Key lerden ile oluştuğu için yani kendine has bir kolonu olmadığı için Primary Key kolonuna ihtiyaç duymaz.
    {
        // Navigation Property
        // 1 like sadece 1 kişi yapmıştır. 1 kişinin çokça like ı olabilir.
        public int AppUserID { get; set; }
        public virtual AppUser AppUser { get; set; }

        // 1 like sadece 1 makale içindir. 1 makalenin çokça like i olabilir.
        public int ArticleID { get; set; }
        public virtual Article Article { get; set; }
    }
}