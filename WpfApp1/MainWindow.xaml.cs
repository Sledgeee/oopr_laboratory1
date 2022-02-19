using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Student> students { get; set; } = new List<Student>();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                string line;
                StreamReader file = new StreamReader("students.txt");
                while ((line = file.ReadLine()) != null)
                {
                    string[] arr = line.Split();
                    Student student = new Student();
                    student.FirstName = arr[0];
                    student.LastName = arr[1];
                    student.Course = arr[2];
                    student.Group = arr[3];
                    student.UkrGrade = int.Parse(arr[4]);
                    student.MathGrade = int.Parse(arr[5]);
                    student.PhysGrade = int.Parse(arr[6]);
                    student.EngGrade = int.Parse(arr[7]);
                    student.MidGrade = (student.UkrGrade + student.MathGrade + student.PhysGrade + student.EngGrade) / 4f;
                    students.Add(student);
                }
                UpdateGrid();
            }
            catch (IOException ex)
            {
                File.Create("students.txt");
            }
        }

        private void UpdateGrid()
        {
            students.Sort((x, y) => {
                return y.MidGrade.CompareTo(x.MidGrade);
            });

            this.StudentsList.Items.Clear();

            foreach (Student s in students)
            {
                this.StudentsList.Items.Add(s);
            }
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student();
            student.FirstName = this.FirstName.Text;
            student.LastName = this.LastName.Text;
            student.Course = this.Course.Text;
            student.Group = this.Group.Text;
            student.UkrGrade = int.Parse(this.UkrGrade.Text);
            student.MathGrade = int.Parse(this.MathGrade.Text);
            student.PhysGrade = int.Parse(this.PhysGrade.Text);
            student.EngGrade = int.Parse(this.EngGrade.Text);
            student.MidGrade = (student.UkrGrade + student.MathGrade + student.PhysGrade + student.EngGrade) / 4f;
            students.Add(student);
            this.StudentsList.Items.Add(student);
            UpdateGrid();
            SaveGrid();
        }

        private void SaveGrid()
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter("students.txt"))
            {
                foreach (Student student in students)
                {
                    file.WriteLine(student.FirstName + " " + student.LastName + " " + student.Course + " " + student.Group +
                        " " + student.UkrGrade + " " + student.MathGrade + " " + student.PhysGrade + " " + student.EngGrade + " " + student.MidGrade);
                }
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.students.Clear();
            UpdateGrid();
            SaveGrid();
        }
    }
}
