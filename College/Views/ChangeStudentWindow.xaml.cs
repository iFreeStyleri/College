using College.Implements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace College.Views
{
    /// <summary>
    /// Логика взаимодействия для ChangeStudentWindow.xaml
    /// </summary>
    public partial class ChangeStudentWindow : Window
    {
        public ChangeStudentWindow(Student student)
        {
            InitializeComponent();
            DataContext = student;
        }

        private void ChangeStudentButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
