using College.Abstractions.Common;
using College.Abstractions.Repositories;
using College.Implements.Models;
using College.Views;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Implements.Commands
{
    public class AsyncChangeStudentCommand : CommandBase
    {
        private readonly ObservableCollection<Student> students;
        private readonly IStudentRepository studentRepository;

        public AsyncChangeStudentCommand(ObservableCollection<Student> students, IStudentRepository studentRepository)
        {
            this.students = students;
            this.studentRepository = studentRepository;
        }
        public override async void Execute(object parameter)
        {
            var student = (Student)parameter;
            var changeWindow = new ChangeStudentWindow(student);
            if (changeWindow.ShowDialog().Value)
            {
                await studentRepository.Update(student);
                var newStudents = await studentRepository.GetAll();
                students.Clear();
                newStudents.ToList().ForEach(student => students.Add(student));
            }
        }

        public override bool CanExecute(object parameter) => parameter != null;
    }
}
