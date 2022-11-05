using BlogProject.Models.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Models.Entities.Concrrete
{
    public class Article : BaseEntity // Makalelerin ID vb ortak özelliklerini alabilmek için BaseEntity den kalıtım alır.
    {

        // Class içerisindeki list yapıları kullanılabilmesi için List yapıları ctor içerisinde instance edilir.
        public Article()
        {
            Likes = new List<Like>();
            Comments = new List<Comment>();
            ArticleCategories = new List<ArticleCategory>();
        }
        public string Title { get; set; }
        public string Content { get; set; }

        // Makale fotoğrafı dosya yolu tutulacağı için string tipinde tutulur.
        public string Image { get; set; }

        [NotMapped] // Bu sınıf configure edilirken NOTMAPPED denirse bu property sql içerisinde kolon olarak ayağa kalkmaz.
        public IFormFile ImagePath { get; set; } // Dosya gönderme ve yükleme işlemlerini yapmak için IFormFile tipi kullanılır. Microsoft.AspNetCore.Http.Features kütüphanesi NuGet Package Manager dan kurulur.
        public int ReadCounter { get; set; }//makalelerin okunma sayısı

        [NotMapped]//sql de kolon ayağa kalkmaması için
        public int? ReadingTime => Content.Length/50;



        // Navigation Propertyler Tanımlanır.

        // 1 makalenin çokça kategorisi olabilir. Bir kategorinin de çokça makalesi olabilir bu yüzden burada çoka çok bir ilişki vardır.Bu yüzden burada bir ara tabloya ihtiyaç duyarız.

        public virtual List<ArticleCategory> ArticleCategories { get; set; } //oluşturduğumuz bu List yapısını da yukarıda tanımlamamız gerekir
       

        // Navigation Property. 1 makalenin 1 yazarı olabilir. Bu durumdan dolayı 1 makalenin 1 yazarını tutmak için int tipinde AppUserID isimli ve AppUser tipinde appUser isimli veri tutulur. 1 yazarın ise çokça makale olabilir. One To Many ilişki
        public int AppUserID { get; set; }
        public virtual AppUser AppUser { get; set; }

        // Navigation Property.  1 makalenin çokça like i olabilir. Bu durumdan dolayı 1 makalenin çokça like tutmak için List<Like> tipinde Likes isimli veri tutulur. 1 Like ise 1 makaleye ait olabilir. One To Many ilişki
        public virtual List<Like> Likes { get; set; }

        // Navigation Property. 1 makalenin çokça comment i olabilir. Bu durumdan dolayı 1 makalenin çokça commebt tutmak için List<Comment> tipinde Comments isimli veri tutulur. 1 Comment ise 1 makaleye ait olabilir. One To Many ilişki
        public virtual List<Comment> Comments { get; set; }
    }
}