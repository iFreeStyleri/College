using College.Abstractions.Common;
using College.Abstractions.Repositories;
using College.Implements.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Implements.Commands
{
    public class AsyncUpdateStudentsCommand : CommandBase
    {
        private readonly ObservableCollection<Student> students;
        private readonly IStudentRepository studentRepository;

        public AsyncUpdateStudentsCommand(ObservableCollection<Student> students, IStudentRepository studentRepository)
        {
            this.students = students;
            this.studentRepository = studentRepository;
        }
        public override async void Execute(object parameter)
        {
            students.Clear();
            var newStudents = await studentRepository.GetAll();
            newStudents.ToList().ForEach(student => students.Add(student));
        }
    }
}
