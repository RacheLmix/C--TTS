using System.Collections.Generic;

namespace Models
{
    public class Student : Person, ICanLearn
    {
        private List<Enrollment> enrollments = new List<Enrollment>();
        public List<Enrollment> Enrollments => enrollments;

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            foreach (var e in enrollments)
            {
                Console.WriteLine($"📚 {e.Course.Name} [{e.Course.Level}] - Điểm: {e.Score}");
            }
        }

        public void RegisterCourse(Course course)
        {
            enrollments.Add(new Enrollment(course));
        }

        public void TakeExam(Course course, double score)
        {
            var e = enrollments.Find(en => en.Course.CourseId == course.CourseId);
            if (e != null)
                e.Score = score;
        }

        public string ToCsv()
        {
            return string.Join("\n", enrollments.Select(e =>
                $"{FullName},{Email},{e.Course.CourseId},{e.Course.Name},{e.Course.Level},{e.Score}"));
        }

        public void Login() => Console.WriteLine($"{FullName} đã đăng nhập.");
        public void Logout() => Console.WriteLine($"{FullName} đã đăng xuất.");
    }
}
