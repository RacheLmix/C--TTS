using Models;

namespace Data
{
    public static class DataManager
    {
        public static void SaveToFile(List<Student> students, string filePath)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(filePath);
                foreach (var s in students)
                    writer.WriteLine(s.ToCsv());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ghi file thất bại: {ex.Message}");
            }
        }

        public static List<Student> LoadFromFile(string filePath)
        {
            var students = new List<Student>();

            try
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length < 6) continue;

                    string fullName = parts[0];
                    string email = parts[1];
                    string courseId = parts[2];
                    string courseName = parts[3];
                    Enum.TryParse(parts[4], out CourseLevel level);
                    double.TryParse(parts[5], out double score);

                    var student = students.FirstOrDefault(s => s.Email == email);
                    if (student == null)
                    {
                        student = new Student { FullName = fullName, Email = email };
                        students.Add(student);
                    }

                    var course = new Course(courseId, courseName, level);
                    student.RegisterCourse(course);
                    student.TakeExam(course, score);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Đọc file thất bại: {ex.Message}");
            }

            return students;
        }
    }
}
