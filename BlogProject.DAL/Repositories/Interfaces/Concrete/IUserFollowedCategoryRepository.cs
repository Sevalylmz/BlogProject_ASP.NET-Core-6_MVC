using BlogProject.Models.Entities.Concrrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DAL.Repositories.Interfaces.Concrete
{
    public interface IUserFollowedCategoryRepository // IBaseRepository den kalıtım almadığı için kendi metot imzaları atılır.
    {
        // Create
        void Create(UserFollowedCategory entity);

        // Delete
        void Delete(UserFollowedCategory entity);

        //List 
        List<UserFollowedCategory> GetUserFollowedCategories(Expression<Func<UserFollowedCategory, bool>> expression);

        //Get One
        UserFollowedCategory GetDefault(Expression<Func<UserFollowedCategory, bool>> expression);
    }
}
