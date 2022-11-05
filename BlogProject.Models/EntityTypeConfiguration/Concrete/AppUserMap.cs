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
    public class AppUserMap  : BaseMap<AppUser> // Oluşturulan BaseMap classından GenericType<Entity> şeklinde kalıtım alarak hangi entity classı için tanımlanacağı söylenir.
    {
        // Configure metodu override edilerek property ler için configuration lar yazılır.

        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // FirstName MaxLength(30) required
            builder.Property(a => a.FirstName).HasMaxLength(30).IsRequired(true);

            // LastName MaxLength(40) required
            builder.Property(a => a.LastName).HasMaxLength(40).IsRequired(true);

            // UserName  required
            builder.Property(a => a.UserName).IsRequired(true);

            // Password  required
            builder.Property(a => a.Password).IsRequired(true);
            builder.Property(a => a.Password1).IsRequired(false);
            builder.Property(a => a.Password2).IsRequired(false);
            builder.Property(a => a.Password3).IsRequired(false);

            // Image MaxLength(200) required
            builder.Property(a => a.Image).HasMaxLength(200).IsRequired(true);

            base.Configure(builder); // ata sınıf yani BaseMap sınıfında yazılan configurationlarda geçerli olsun diye bırakılır. Burada yazılan builder configurationları alıp atasında bulunan Configure metoduna giriş parametresi olarak götürür.
        }
    }
}
