using College.Abstractions.Common;
using College.Abstractions.Repositories;
using College.DAO.Repositories;
using College.Implements.Models;
using College.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Implements.Commands
{
    public class AsyncAddStudentCommand : CommandBase
    {
        private readonly ObservableCollection<Student> students;
        private readonly IStudentRepository studentRepository;

        public AsyncAddStudentCommand(ObservableCollection<Student> students, IStudentRepository studentRepository)
        {
            this.students = students;
            this.studentRepository = studentRepository;
        }
        public override async void Execute(object parameter)
        {
            var student = new Student();
            var addWindow = new AddStudentWindow(student);
            student.DateOfBirthday = DateTime.Now;
            if (addWindow.ShowDialog().Value)
            {
                var today = DateTime.Today;
                var age = today.Year - student.DateOfBirthday.Year;
                student.Age = age;
                await studentRepository.Insert(student);
                student.Id = students.Last().Id + 1;
                students.Add(student);
            }
        }
    }
}
