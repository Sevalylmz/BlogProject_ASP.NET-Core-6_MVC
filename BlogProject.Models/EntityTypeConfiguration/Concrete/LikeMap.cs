using BlogProject.Models.Entities.Concrrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Models.EntityTypeConfiguration.Concrete
{
    public class LikeMap : IEntityTypeConfiguration<Like> // BaseMap classından kalıtım almamaktadır. Çünkü beğeni için CRUD operasyonlarının tümü geçerli değildir. ID almadığı için AppUserID ve ArticleID composite key olacaktır. Yani tekrarlar yapılamayacaktır. Ayrıca bu tablo sadece diğer tablolardan aldığı Foreing Key lerden ile oluştuğu için yani kendine has bir kolonu olmadığı için Primary Key kolonuna ihtiyaç duymaz.
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {

            builder.HasKey(a => new { a.AppUserID, a.ArticleID }); // Primary Key kolonu olmadığı için yazılır

            // Navigation Property. 1 like 1 kullanıcıya aittir. 1 kullanıcının birçok like olabilir. One to Many
            // Burada 1 like nesnesi yalnızca 1 kullanıcı nesnesine sahiptir. 1 kullanıcı nesnesi ise bir çok like nesnesine sahiptir ve like tablosunda AppUserID kolonu foreign keydir diye açıklama yapılır. 
            builder.HasOne(a => a.AppUser).WithMany(a => a.Likes).HasForeignKey(a => a.AppUserID);

            // Navigation Property. 1 like 1 makaleye aittir. 1 makalenin birçok like olabilir. One to Many
            // Burada 1 like nesnesi yalnızca 1 makale nesnesine sahiptir. 1 makale nesnesi ise bir çok like nesnesine sahiptir ve like tablosunda ArticleID kolonu foreign keydir diye açıklama yapılır. 
            builder.HasOne(a => a.Article).WithMany(a => a.Likes).HasForeignKey(a => a.ArticleID);
        }
    }
}
