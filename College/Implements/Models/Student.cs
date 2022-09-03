using College.Abstractions.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace College.Implements.Models
{
    public class Student : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirthday { get; set; }

        public IList<SqliteParameter> GetSqlParameters() 
            => new List<SqliteParameter>
            {
                new SqliteParameter("@id", Id),
                new SqliteParameter("@name", Name),
                new SqliteParameter("@lastName", LastName),
                new SqliteParameter("@patronymic", Patronymic),
                new SqliteParameter("@age", Age),
                new SqliteParameter("@dateOfBirthday", DateOfBirthday)
            };
    }
}
