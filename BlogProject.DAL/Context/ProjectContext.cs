using BlogProject.Models.Entities.Concrrete;
using BlogProject.Models.EntityTypeConfiguration.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DAL.Context
{
    public class ProjectContext : IdentityDbContext // Identity kütüphanesi kullanılarak kullanıcı işlemleri de yapılacağı için IdentityDbContext sınıfından kalıtım alacaktır. Bu sayede hem Microsoft Identity kütüphanesinin otomatik oluşturacağı tablolar hemde bizim oluşturduğumuz tablolar aynı database içerisinde olacaktır.

    {
        // Context sınıfı ayağa kalkerken parametrelerini kendi atası olan IdentityDbContext e göndererek ayağa kalkacaktır. 

        public ProjectContext(DbContextOptions options) : base(options)
        {

        }

        // Database içerisinde oluşması istenilen tablo lar DbSet<EntityName> TableName şeklinde tanımlanır.
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFollowedCategory> UserFollowedCategories { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }

        // EntityTypeConfiguration sınıflarımız kullanılarak tabloların oluşturulması için OnModelCreating metodu override edilir.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new ArticleMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new CommentMap());
            builder.ApplyConfiguration(new IdentityRoleMap());
            builder.ApplyConfiguration(new LikeMap());
            builder.ApplyConfiguration(new UserFollowedCategoryMap());
            builder.ApplyConfiguration(new ArticleCategoryMap());
            base.OnModelCreating(builder);
        }

    }

}



