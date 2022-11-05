using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Models.EntityTypeConfiguration.Concrete
{
    public class IdentityRoleMap : IEntityTypeConfiguration<IdentityRole> // Kullanıcılar için roller Microsoft.AspNetCore.Identity kütüphanesinde hazır olarak gelen IdentityRole sınıfı kullanılarak yapılacağı için bu sınıfın configuration atamaları yapılır
    {
        // Burada seed data olarak Rolleri hazır olarak getireceğiz. Bunun için Roller, Configure metodu içerisinde tanımlanır. Member ve Admin isimli sadece 2 adet Rol vardır.
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Member",
                NormalizedName = "MEMBER"
            },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
        }
    }
}
