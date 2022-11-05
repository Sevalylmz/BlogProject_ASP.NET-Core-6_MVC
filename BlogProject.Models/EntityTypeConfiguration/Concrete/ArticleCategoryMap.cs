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
    public class ArticleCategoryMap : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            // Navigation Property. 1 kategorinin birçok makalesi vardır. 1 makalenin birçok kategori olabilir. Many to Many
            // Bu tabloda ArticleID kolonu foreign keydir diye açıklama yapılır. Ayrıca CategoryID de foreign Key dir.

            builder.HasKey(a => new { a.ArticleID, a.CategoryID }); // Primary Key kolonu olmadığı için yazılır

            builder.HasOne(a => a.Article).WithMany(a => a.ArticleCategories).HasForeignKey(a => a.ArticleID);

            builder.HasOne(a => a.Category).WithMany(a => a.ArticleCategories).HasForeignKey(a => a.CategoryID);

        }
    }
}
