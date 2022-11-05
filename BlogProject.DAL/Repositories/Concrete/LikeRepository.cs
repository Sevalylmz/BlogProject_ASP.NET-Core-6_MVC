using BlogProject.DAL.Context;
using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.Models.Entities.Concrrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DAL.Repositories.Concrete
{
    public class LikeRepository : ILikeRepository // LikeRepository ortak olan CRUD işlemlerini yapmadığı için BaseRepositoryden kalıtım almaz sadece kendi interface inden kalıtım alır. BaseRepositoryden kalıtım almadığı için Context bağlantısı CTOR içerisinde ayrıca yapılmalıdır.
    {

        // Veri tabanındaki CRUD işlemlerini yapmak için yazılacak metotların çalışması için veri tabanına bağlantının yapılmış olması gerekiyor. Yani Repository çağrıldığında database bağlantısının yapılmış olması gerekiyor. Bu durumdan dolayı Repository classının ctor içerisinde tanımlanır. SOLID in D prensibi gereği Constructor Injection yapılır.
        // IOC pattern deseni CORE için kullanılır. Özellikle araştır.

        // Bir class içerisindeki propertylere ulaşmak için o sınıfın instance alınması gerekmektedir. Context sınıfı içerisindeki propertylere DbSet<Entity> yani database de ki tablolara ulaşmak için Ctor un giriş parametresinde ProjectContext sınıfı tanımlanır.
        private readonly ProjectContext _context; // Database nesnesi
        private readonly DbSet<Like> _table; // Tablo nesnesi

        public LikeRepository(ProjectContext context)
        {
            _context = context;
            _table = context.Set<Like>(); // Tablo nesnesi database in Set<Like>() metodu ile doldurulur.
        }

        // Create
        public void Create(Like entity)
        {
            _table.Add(entity);
            _context.SaveChanges();
        }

        // Delete
        public void Delete(Like entity)
        {
            _table.Remove(entity);
            _context.SaveChanges();
        }

        // Get One
        public Like GetDefault(Expression<Func<Like, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }
    }
}
