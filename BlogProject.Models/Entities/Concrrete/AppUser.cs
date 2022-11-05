using BlogProject.Models.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Models.Entities.Concrrete
{
    public class AppUser : BaseEntity // Kullanıcılarında ID vb ortak özelliklerini alabilmek için BaseEntity den kalıtım alır.
    {
        // Class içerisindeki list yapıları kullanılabilmesi için List yapıları ctor içerisinde instance edilir.
        public AppUser()
        {
            Articles = new List<Article>(); //aşağıda list yapılarını tanımlıyoruz burada da oluşturuyoruz
            Comments = new List<Comment>();
            Likes = new List<Like>();
            UserFollowedCategories = new List<UserFollowedCategory>();

        }

        // AppUser a ait propertyler tanımlanır.
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string Password3 { get; set; }

        // Identity tarafıyla kullanıcıyı eşleştirmek için AppUser içinde IdentityId isimli property tanımlanır. Identity tarafı Id değerini GUID string paylaştığı için bu propertyde string tipinde tanımlanır.
        public string IdentityId { get; set; }

        // FullName property tanımlanır.
        public string FullName => $"{FirstName} {LastName}";

        // Profil fotoğrafı dosya yolu tutulacağı için string tipinde tutulur.
        public string Image { get; set; }

        [NotMapped] // Bu sınıf configure edilirken NOTMAPPED denirse bu property sql içerisinde kolon olarak ayağa kalkmaz.
        public IFormFile ImagePath { get; set; } // Dosya gönderme ve yükleme işlemlerini yapmak için IFormFile tipi kullanılır. Microsoft.AspNetCore.Http.Features kütüphanesi NuGet Package Manager dan kurulur.

        // Navigation Property. Eager Loading kullanılacaktı ama ben lazye çevirdim bu yüzden Sorgular yazılırken Include kullanılarak yazılmadı. 1 yazarın çokça makalesi olabilir. Bu durumdan dolayı 1 yazarın birçok makalesini tutmak için List<Article> tipinde veri tutulur. 1 makalenin ise yalnızca 1 yazarı olur. One To Many ilişki
        public virtual List<Article> Articles { get; set; }

        // Navigation Property. 1 yazarın çokça yorumu olabilir. Bu durumdan dolayı 1 yazarın birçok yorumunu tutmak için List<Comment> tipinde veri tutulur. 1 yorumun ise yalnızca 1 yazarı olur. One To Many ilişki
        public virtual List<Comment> Comments { get; set; }

        // Navigation Property.  1 yazarın çokça like i olabilir. Bu durumdan dolayı 1 yazarın birçok like tutmak için List<Like> tipinde veri tutulur. 1 like in ise yalnızca 1 yazarı olur. One To Many ilişki
        public virtual List<Like> Likes { get; set; }

        // Navigation Property.  1 yazarın çokça takip ettiği kategori  olabilir ya da bir kategoriyi çokça yazar takip edebilir. Bu durumdan dolayı 1 yazarın birçok kategoriyi tutmak için List<UserFollowedCategory> tipinde veri tutulur. Many To Many ilişki. Çoka çok ilişkiden dolayı ara tablo oluşacaktır.
        public virtual List<UserFollowedCategory> UserFollowedCategories { get; set; }


    }
}
