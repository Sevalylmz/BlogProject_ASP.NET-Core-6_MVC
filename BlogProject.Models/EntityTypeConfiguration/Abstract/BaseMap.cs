using BlogProject.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Models.EntityTypeConfiguration.Abstract
{
    //configürasyonlar benim modelde oluşturduğum propertilerin sqldeki tablolarını oluşturmak için yapılır.Bütün propertileri alma zorunluluğumuz yoktur biz neyi görmek istiyorsak onları yazarız.
    public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity // Kendinden kalıtım alacak tiplere göre işlem yapabilmek için GenericType<T> olarak kullanılacaktır.
    {
        public virtual void Configure(EntityTypeBuilder<T> builder) // Bu classdan kalıtım alan sınıflarda kendi configurationlarını tanımlamak için bu metot virtual olarak işaretlenir ve kalıtım verdiği sınıf içerisinde override edilerek kullanılır.
        {
            // ID kolonu primary key olarak seçilir.
            builder.HasKey(a => a.ID);

            // CreateDate required. Date formatlar söylenmediği taktirde datetime2 olarak alınır.
            builder.Property(a => a.CreateDate).IsRequired(true);
            // ModifiedDate nullable
            builder.Property(a => a.ModifiedDate).IsRequired(false);
            // RemovedDate nullable
            builder.Property(a => a.RemovedDate).IsRequired(false);
            // Statu required
            builder.Property(a => a.Statu).IsRequired(true);

        }
    }
}
