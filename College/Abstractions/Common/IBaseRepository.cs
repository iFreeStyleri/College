using College.Abstractions.Models;
using College.Implements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Abstractions.Common
{
    public interface IBaseRepository<TModel> where TModel : IEntity 
    {
        Task<IList<TModel>> GetAll();
        Task<TModel> Get(int id);
        Task Insert(TModel model);
        Task Delete(int id);
        Task Update(Student model);
    }
}
