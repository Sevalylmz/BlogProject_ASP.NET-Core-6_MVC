using BlogProject.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DAL.Repositories.Interfaces.Abstract
{
    public interface IBaseRepository<T> where T : BaseEntity // Bu interface BaseEntity classının CRUD işlemlerini yapacağı için ve diğer interfacelere kalıtım vereceği için GenericType olarak tanımlandı.
    {
        // Create
        void Create(T entity);

        // Update
        void Update(T entity);

        // Delete
        void Delete(T entity);

        // Any
        bool Any(Expression<Func<T, bool>> expression);

        // Select One
        T GetDefault(Expression<Func<T, bool>> expression); //tek bir seçim 

        // Select Many
        List<T> GetDefaults(Expression<Func<T, bool>> expression); //bir çok seçim

        // Get By Default. Bu metot kullanıldığında seçim(selector) için bir linq sorgusu, genel bir seçim için expression parametresi ve ensonda boş da bırakılabilir bir - sorgulanabilir ve dahil edilebilir bir expression daha yazılabilir.

        // dönüş tipine karar veremediğimiz / daha kompleks tip dönüşümleri için yukarıdaki doğrudan T dönen metotları değil TRESULT sonucu dönen kendi yazdığımız metotları kullanabiliriz. TRESULT tipi var dönüş tipi gibi kendi çıkış parametresine döner.
        // Farklı tabloların include edildiği ve sorgulanabildiği Eager Loading için yazılan metottur.
        TResult GetByDefault<TResult>
            (
            Expression<Func<T, TResult>> selector, // seçim
            Expression<Func<T, bool>> expression, // sorgu
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null // içermesini istediğimiz başka tablolar / include ettiğimiz yani dahil ettiğimiz sınıflar varsa (opsiyonel)
            );

        // Get By Defaults. Bu metot kullanıldığında seçim(selector) için bir linq sorgusu, genel bir seçim için expression parametresi, boş da bırakılabilir bir - sorgulanabilir ve dahil edilebilir bir expression ve ensonda sıralanabilir sıralayıcı daha yazılabilir. Birçok veri döneceği için List<TResult> çıkış parametresi olur.

        // dönüş tipine karar veremediğimiz / daha kompleks tip dönüşümleri için yukarıdaki doğrudan T dönen metotları değil TRESULT sonucu dönen kendi yazdığımız metotları kullanabiliriz. TRESULT tipi var dönüş tipi gibi kendi çıkış parametresine döner.
        // Farklı tabloların include edildiği ve sorgulanabildiği Eager Loading için yazılan metottur.
        List<TResult> GetByDefaults<TResult>
            (
            Expression<Func<T, TResult>> selector, // seçim
            Expression<Func<T, bool>> expression, // sorgu
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, // içermesini istediğimiz başka tablolar / include ettiğimiz yani dahil ettiğimiz sınıflar varsa (opsiyonel). Eager Loading yapıldığı için kullanılır.
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null // Sıralama varsa (opsiyonel)
            );

        //Admin Approve (onayladığı durum) için imza atılıyor

        void Approve(T entity);

        //Admin Reject (onaylamadığı durum) için imza atılıyor

        void Reject(T entity);
    }
}
