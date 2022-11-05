using BlogProject.DAL.Context;
using BlogProject.DAL.Repositories.Interfaces.Abstract;
using BlogProject.Models.Entities.Abstract;
using BlogProject.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DAL.Repositories.Abstract
{

    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity // IBaseRepository interface içerisinde imzalanan metotların gövdeleri yazılacağı için IBaseRepository interfaceinden kalıtım alır. Bu sınıf diğer sınıflara kalıtım vereceği için public abstract class olarak yazılır ve ayrıca GenericType<T> olarak düzenlenir.
    {


        // Veri tabanındaki CRUD işlemlerini yapmak için yazılacak metotların çalışması için veri tabanına bağlantının yapılmış olması gerekiyor. Yani Repository çağrıldığında database bağlantısının yapılmış olması gerekiyor. Bu durumdan dolayı Repository classının ctor içerisinde tanımlanır. SOLID in D prensibi gereği Constructor Injection yapılır.
        // IOC pattern deseni CORE için kullanılır. Özellikle araştır.

        // Bir class içerisindeki propertylere ulaşmak için o sınıfın instance alınması gerekmektedir. Context sınıfı içerisindeki propertylere DbSet<Entity> yani database de ki tablolara ulaşmak için Ctor un giriş parametresinde ProjectContext sınıfı tanımlanır.

        protected readonly ProjectContext _context; // Database nesnesi bunu protected a çevirisek kalıtım alan sınıflar da görebilir.
        protected readonly DbSet<T> _table; // Tablo nesnesi,bunu da protected yaptım ki kalıtım alan sınıflar da görebilsin ve ben bunu readcounter için yaptım.
        public BaseRepository(ProjectContext context)
        {
            _context = context;
            _table = context.Set<T>(); // Tablo nesnesi database in Set<T>() metodu ile doldurulur.
        }

        // Any. içeride birşey var mı ?
        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _table.Any(expression);
        }

        // Create
        public void Create(T entity)
        {
            _table.Add(entity);
            _context.SaveChanges();
                  }

        // Delete
        public void Delete(T entity)
        {
            entity.Statu = Statu.Passive;
            entity.RemovedDate = DateTime.Now;
            _context.SaveChanges();

        }

        // GetByDefault
        public TResult GetByDefault<TResult>//tresult bilmediğimiz bir tipler için kullanmamız lazım
            (
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
            )
        {
            IQueryable<T> query = _table; // Tablomuzu sorgulanabilir T tipini barındıran bir tablo olarak atadık

            if (include != null) // Include içeren sorgu doluysa
            {
                query = include(query); // include ait işlemleri yap
            }
            if (expression != null) // expression doluysa
            {
                query = query.Where(expression); // sorgula
            }
            return query.Select(selector).First(); // include ve expression sorgusu dolu veya boş gelsede en sonda bu seçim işlemi yapılacak. Tek bir veri alınacağı için First metodu kullanılır.
        }

        // GetByDefaults
        public List<TResult> GetByDefaults<TResult>
            (
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null
            )
        {
            IQueryable<T> query = _table; // Tablomuzu sorgulanabilir T tipini barındıran bir tablo olarak atadık

            if (include != null) // include doluysa
            {
                query = include(query); // include ait işlemleri yap
            }

            if (expression != null) // expression doluysa
            {
                query = query.Where(expression); // sorgulama işlemlerini yap
            }
            if (orderby != null) // orderby doluysa
            {
                return orderby(query).Select(selector).ToList(); // sıralamaya ait işlemleri yap ve listeyi döndür
            }
            return query.Select(selector).ToList(); // şartlar sağlansada sağlanmasada en son bu seçim işlemini yap. Birçok veri geleceği için ToList() metodu kullanılır.
        }

        // GetDefault. Tek bir değer döndürür.
        public T GetDefault(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }

        // GetDefaults. Liste döndürür.
        public List<T> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }

        // Update
        public void Update(T entity)
        {
            entity.Statu = Statu.Modified;
            entity.ModifiedDate = DateTime.Now;
            _table.Update(entity);
            _context.SaveChanges();
        }
        //Admin onayı (Approved) için gövdesini tutuyoruz (repo)

        public void Approve(T entity)
        {
            entity.Statu = Statu.Active;
            entity.AdminCheck = AdminCheck.Approved;
            _table.Update(entity);
            _context.SaveChanges();

        }

        //Adminin onaylamadığı (Reject) durumlar için gövde tutuyoruz (repo)

        public void Reject(T entity)
        {
            entity.Statu = Statu.Passive;
            entity.AdminCheck = AdminCheck.Rejected;
            _table.Update(entity);
            _context.SaveChanges();

        }



    }
}
