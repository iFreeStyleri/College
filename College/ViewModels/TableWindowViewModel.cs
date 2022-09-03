using College.Abstractions.Common;
using College.DAO.Repositories;
using College.Implements.Commands;
using College.Implements.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace College.ViewModels
{
    public class TableWindowViewModel : ViewModelBase
    {
        private readonly StudentRepository studentRepository;
        public ObservableCollection<Student> Students { get; set; }
        public ICommand UpdateStudentsCommand { get; }
        public ICommand AddStudentCommand { get; }
        public ICommand RemoveStudentCommand { get; }
        public ICommand ChangeStudentCommand { get; }
        public TableWindowViewModel()
        {
            Students = new();
            studentRepository = new StudentRepository();
            UpdateStudentsCommand = new AsyncUpdateStudentsCommand(Students, studentRepository);
            AddStudentCommand = new AsyncAddStudentCommand(Students, studentRepository);
            RemoveStudentCommand = new AsyncRemoveStudentCommand(Students, studentRepository);
            ChangeStudentCommand = new AsyncChangeStudentCommand(Students, studentRepository);
            Init();
        }

        private async void Init()
        {
            var students = await studentRepository.GetAll();
            students.ToList().ForEach(student => Students.Add(student));
        }
    }
}
