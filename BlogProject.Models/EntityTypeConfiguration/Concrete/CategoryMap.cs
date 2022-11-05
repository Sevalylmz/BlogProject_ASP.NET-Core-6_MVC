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
    public class CategoryMap : BaseMap<Category> // Oluşturulan BaseMap classından GenericType<Entity> şeklinde kalıtım alarak hangi entity classı için tanımlanacağı söylenir.
    {
        // Configure metodu override edilerek property ler için configuration lar yazılır.
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            // Name  Required
            builder.Property(a => a.Name).IsRequired(true);

            // Description Required
            builder.Property(a => a.Description).IsRequired(true);

            // Navitaion Property ler için Artcile ile arasındaki ilişki ArticleMap içerisinde tanımlandığı için burada tanımlanmaya gerek yoktur.

            // Article alakalı olan ilişkisi içinde ara tablo içinde ilişkisi tanımlanacaktır.

            base.Configure(builder); // ata sınıf yani BaseMap sınıfında yazılan configurationlarda geçerli olsun diye bırakılır. Burada yazılan builder configurationları alıp atasında bulunan Configure metoduna giriş parametresi olarak götürür.
        }
    }
}
