using College.Abstractions.Common;
using College.Abstractions.Repositories;
using College.Implements.Models;
using College.Properties;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace College.DAO.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SqliteConnection sqlConnection;
        public StudentRepository()
        {
            sqlConnection = new SqliteConnection(Resources.ConnectionString);
            CreateTable();
        }
        private async Task<bool> TableIsExists() 
        {
            await sqlConnection.OpenAsync();
            using var command = new SqliteCommand("SELECT EXISTS (SELECT name FROM sqlite_master WHERE type='table' AND name='Students');", sqlConnection);
            var count = (long)await command.ExecuteScalarAsync();
            await sqlConnection.CloseAsync();
            return count > 0;
        }
        private async void CreateTable()
        {
            if (await TableIsExists())
                return;
            await sqlConnection.OpenAsync();
            using var command = new SqliteCommand("CREATE TABLE Students(" +
                "Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                "Name TEXT NOT NULL, " +
                "LastName TEXT NOT NULL, " +
                "Patronymic TEXT NOT NULL, " +
                "Age INTEGER NOT NULL," +
                "DateOfBirthday DATETIME NOT NULL)", sqlConnection);
            await command.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public async Task Delete(int id)
        {
            await sqlConnection.OpenAsync();
            using var command = new SqliteCommand("DELETE FROM Students WHERE Id = @id", sqlConnection);
            var idParameter = new SqliteParameter("@id", id);
            command.Parameters.Add(idParameter);
            await command.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public async Task<Student> Get(int id)
        {
            await sqlConnection.OpenAsync();
            using var command = new SqliteCommand("SELECT * FROM Students WHERE Id = @Id", sqlConnection);
            var idParameter = new SqliteParameter("@Id", id);
            command.Parameters.Add(idParameter);
            using var reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();
            await sqlConnection.CloseAsync();
            return new Student
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                LastName = reader.GetString(2),
                Patronymic = reader.GetString(3),
                Age = reader.GetInt32(4),
                DateOfBirthday = reader.GetDateTime(5),
            };
        }

        public async Task<IList<Student>> GetAll()
        {
            await sqlConnection.OpenAsync();
            var list = new List<Student>();
            using var command = new SqliteCommand("SELECT * FROM Students", sqlConnection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var student = new Student
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Patronymic = reader.GetString(3),
                    Age = reader.GetInt32(4),
                    DateOfBirthday = reader.GetDateTime(5),
                };
                list.Add(student);
            }
            await sqlConnection.CloseAsync();
            return list;
        }

        public async Task Insert(Student model)
        {
            await sqlConnection.OpenAsync();
            var command = new SqliteCommand(
                "INSERT INTO Students (Name, LastName, Patronymic, Age, DateOfBirthday) " +
                "VALUES (@name, @lastName, @patronymic, @age, @dateOfBirthday)", sqlConnection);
            command.Parameters.AddRange(model.GetSqlParameters().ToArray());
            await command.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }
        public async Task Update(Student model)
        {
            await sqlConnection.OpenAsync();
            var command = new SqliteCommand(
                "UPDATE Students " +
                "SET Name=@name, LastName=@lastName, Patronymic=@patronymic, Age=@age, DateOfBirthday = @dateOfBirthday " +
                "WHERE Id=@id", sqlConnection);
            command.Parameters.AddRange(model.GetSqlParameters().ToArray());
            await command.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection.Dispose();
        }
    }
}
