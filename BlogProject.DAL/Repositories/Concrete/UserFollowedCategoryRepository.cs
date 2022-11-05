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
    public class UserFollowedCategoryRepository : IUserFollowedCategoryRepository// UserFollowedCategory ortak olan CRUD işlemlerini yapmadığı için BaseRepositoryden kalıtım almaz sadece kendi interface inden kalıtım alır. BaseRepositoryden kalıtım almadığı için Context bağlantısı CTOR içerisinde ayrıca yapılmalıdır.
    {
        // Veri tabanındaki CRUD işlemlerini yapmak için yazılacak metotların çalışması için veri tabanına bağlantının yapılmış olması gerekiyor. Yani Repository çağrıldığında database bağlantısının yapılmış olması gerekiyor. Bu durumdan dolayı Repository classının ctor içerisinde tanımlanır. SOLID in D prensibi gereği Constructor Injection yapılır.
        // IOC pattern deseni CORE için kullanılır. Özellikle araştır.

        // Bir class içerisindeki propertylere ulaşmak için o sınıfın instance alınması gerekmektedir. Context sınıfı içerisindeki propertylere DbSet<Entity> yani database de ki tablolara ulaşmak için Ctor un giriş parametresinde ProjectContext sınıfı tanımlanır.

        private readonly ProjectContext _context; // Database nesnesi
        private readonly DbSet<UserFollowedCategory> _table; // Tablo nesnesi

        public UserFollowedCategoryRepository(ProjectContext context)
        {
            _context = context;
            _table = context.Set<UserFollowedCategory>(); // Tablo nesnesi database in Set<Like>() metodu ile doldurulur.
        }

        // Create
        public void Create(UserFollowedCategory entity)
        {
            _table.Add(entity);
            _context.SaveChanges();
        }

        // Delete
        public void Delete(UserFollowedCategory entity)
        {
            _table.Remove(entity);
            _context.SaveChanges();
        }

        // ToDo : List<int> yazmak için Func<int,bool> nasıl düzenlenecek
        public List<UserFollowedCategory> GetUserFollowedCategories(Expression<Func<UserFollowedCategory, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }

        // Get One
        public UserFollowedCategory GetDefault(Expression<Func<UserFollowedCategory, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }

       
    }
}
