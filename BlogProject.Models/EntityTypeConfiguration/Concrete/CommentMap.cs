using BlogProject.Models.Entities.Concrrete;
using BlogProject.Models.EntityTypeConfiguration.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Models.EntityTypeConfiguration.Concrete
{
    public class CommentMap : BaseMap<Comment> // Oluşturulan BaseMap classından GenericType<Entity> şeklinde kalıtım alarak hangi entity classı için tanımlanacağı söylenir.
    {
        // Configure metodu override edilerek property ler için configuration lar yazılır.
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            // Text Required
            builder.Property(a => a.Text).IsRequired(true);

            // Navigation Property. 1 yorum 1 kullanıcıya aittir. 1 kullanıcının birçok yorumu olabilir. One to Many
            // Burada 1 yorum nesnesi yalnızca 1 kullanıcı nesnesine sahiptir. 1 kullanıcı nesnesi ise bir çok yorum nesnesine sahiptir ve yorum tablosunda AppUserID kolonu foreign keydir diye açıklama yapılır. 
            builder.HasOne(a=>a.AppUser).WithMany(a=>a.Comments).HasForeignKey(a=>a.AppUserID);

            // Navigation Property. 1 yorum 1 makaleye aittir. 1 makalenin birçok yorumu olabilir. One to Many
            // Burada 1 yorum nesnesi yalnızca 1 makale nesnesine sahiptir. 1 makale nesnesi ise bir çok yorum nesnesine sahiptir ve yorum tablosunda ArticleID kolonu foreign keydir diye açıklama yapılır. 
            builder.HasOne(a => a.Article).WithMany(a => a.Comments).HasForeignKey(a => a.ArticleID);

            base.Configure(builder); // ata sınıf yani BaseMap sınıfında yazılan configurationlarda geçerli olsun diye bırakılır. Burada yazılan builder configurationları alıp atasında bulunan Configure metoduna giriş parametresi olarak götürür.
        }
    }
}
