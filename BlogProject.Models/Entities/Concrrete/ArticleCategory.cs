using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Models.Entities.Concrrete
{
    public class ArticleCategory // BaseEntity classından kalıtım almamaktadır. Çünkü ara tablo olduğu için CRUD operasyonlarının tümü geçerli değildir. ID almadığı için ArticleID ve CategoryID composite key olacaktır. Yani tekrarlar yapılamayacaktır. Ayrıca bu tablo sadece diğer tablolardan aldığı Foreing Key lerden ile oluştuğu için yani kendine has bir kolonu olmadığı için Primary Key kolonuna ihtiyaç duymaz.
    {
        public int ArticleID { get; set; }
        public virtual Article Article { get; set; }


        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}
