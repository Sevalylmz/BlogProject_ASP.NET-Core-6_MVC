using BlogProject.Models.Entities.Concrrete;
using BlogProject.Models.EntityTypeConfiguration.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Models.EntityTypeConfiguration.Concrete
{
    public class ArticleMap : BaseMap<Article> // Oluşturulan BaseMap classından GenericType<Entity> şeklinde kalıtım alarak hangi entity classı için tanımlanacağı söylenir.
    {
        // Configure metodu override edilerek property ler için configuration lar yazılır.
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            // Title MaxLength(120) Required
            builder.Property(a => a.Title).HasMaxLength(120).IsRequired(true);

            // Content Required
            builder.Property(a => a.Content).IsRequired(true);

            // Image Required             
            builder.Property(a => a.Image).IsRequired(true);

            

            // Navigation Property. Configurationlarda açıklanırken ilişkili tablolar arasından sadece birinde açıklanması yeterlidir.

            // 1 makaleyi sadece 1 kullanıcı yazmıştır. 1 kullanıcının birçok makalesi vardır. One To Many
            // Burada 1 makale nesnesi yalnızca 1 kullanıcı nesnesine sahiptir. 1 kullanıcı nesnesi ise bir çok makale nesnesine sahiptir ve makale tablosunda AppUserID kolonu foreign keydir diye açıklama yapılır. 
            //DeleteBehavior.Restrict => ebeveyn - coçuk ilişkisi gibi düşünülebilir. Yani makale silindiğinde sıkıntı yok ama makalenin sahibi olan User silinirse hata verir. Makaleleri silebilirsiniz ama User silmeye çalışırsanız ve User in makaleleri varsa o Useri silemezsiniz çünkü o Makaleler Usersiz olmaz.
            builder.HasOne(a => a.AppUser).WithMany(a => a.Articles).HasForeignKey(a => a.AppUserID).OnDelete(DeleteBehavior.Restrict);


           

            base.Configure(builder); // ata sınıf yani BaseMap sınıfında yazılan configurationlarda geçerli olsun diye bırakılır. Burada yazılan builder configurationları alıp atasında bulunan Configure metoduna giriş parametresi olarak götürür.
        }
    }
}
