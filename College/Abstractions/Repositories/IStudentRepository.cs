using College.Abstractions.Common;
using College.Implements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Abstractions.Repositories
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
    }
}
