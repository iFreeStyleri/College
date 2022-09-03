using College.Abstractions.Common;
using College.Abstractions.Repositories;
using College.DAO.Repositories;
using College.Implements.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace College.Implements.Commands
{
    public class AsyncRemoveStudentCommand : CommandBase
    {
        private readonly ObservableCollection<Student> students;
        private readonly IStudentRepository studentRepository;

        public AsyncRemoveStudentCommand(ObservableCollection<Student> students, IStudentRepository studentRepository)
        {
            this.students = students;
            this.studentRepository = studentRepository;
        }
        public override async void Execute(object parameter)
        {
            var student = (Student)parameter;
            await studentRepository.Delete(student.Id);
            students.Remove(student);
        }
        public override bool CanExecute(object parameter) => parameter != null;

    }
}
