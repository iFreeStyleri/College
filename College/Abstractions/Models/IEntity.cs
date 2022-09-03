using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace College.Abstractions.Models
{
    public interface IEntity
    {
        IList<SqliteParameter> GetSqlParameters();
    }
}
