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
    public class UserFollowedCategoryMap : IEntityTypeConfiguration<UserFollowedCategory> // BaseMap classının configuration ettiği BaseEntity sınıfından kalıtım almadığı için BaseMap classındanda kalıtım almaz.
    {
        public void Configure(EntityTypeBuilder<UserFollowedCategory> builder)
        {
            // Navigation Property. 1 kategori birçok kullanıcıya aittir. 1 kullanıcının birçok kategori olabilir. Many to Many
            // Burada 1 kategori nesnesi birçok kullanıcı nesnesine sahiptir. 1 kullanıcı nesnesi de bir çok kategori nesnesine sahiptir ve UserFollowedCategory ara tablosu oluşacaktır. Bu tabloda AppUserID kolonu foreign keydir diye açıklama yapılır. Ayrıca CategoryID de foreign Key dir.

            builder.HasKey(a => new { a.AppUserID, a.CategoryID }); // Primary Key kolonu olmadığı için yazılır

            builder.HasOne(a=>a.AppUser).WithMany(a=>a.UserFollowedCategories).HasForeignKey(a=>a.AppUserID);
              
            builder.HasOne(a => a.Category).WithMany(a => a.UserFollowedCategories).HasForeignKey(a => a.CategoryID);
            
        }
    }
}
