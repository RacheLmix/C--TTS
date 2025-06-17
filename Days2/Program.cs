using Models;
using Data;

List<Student> students = new();
List<Course> courses = new()
{
    new Course("C01", "Lập trình C#", CourseLevel.Beginner),
    new Course("C02", "Lập trình Web", CourseLevel.Intermediate),
    new Course("C03", "Lập trình AI", CourseLevel.Advanced),
};

while (true)
{
    Console.WriteLine("\n🎓 MENU:");
    Console.WriteLine("1. Thêm học viên");
    Console.WriteLine("2. Đăng ký khóa học");
    Console.WriteLine("3. Nhập điểm");
    Console.WriteLine("4. Hiển thị danh sách");
    Console.WriteLine("5. Ghi dữ liệu ra file");
    Console.WriteLine("6. Đọc dữ liệu từ file");
    Console.WriteLine("0. Thoát");
    Console.Write("👉 Chọn: ");

    string? choice = Console.ReadLine();
    Console.WriteLine();

    try
    {
        switch (choice)
        {
            case "1":
                Console.Write("Họ tên: ");
                string? nameInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nameInput)) throw new Exception("Tên không hợp lệ.");
                string name = nameInput.Trim();

                Console.Write("Email: ");
                string? emailInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(emailInput)) throw new Exception("Email không hợp lệ.");
                string email = emailInput.Trim().ToLower();

                students.Add(new Student { FullName = name, Email = email });
                Console.WriteLine("✅ Đã thêm học viên.");
                break;

            case "2":
                Console.Write("Nhập email học viên: ");
                string? regEmailInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(regEmailInput)) throw new Exception("Email không hợp lệ.");
                string regEmail = regEmailInput.Trim().ToLower();

                var student = students.Find(s => s.Email.ToLower() == regEmail);
                if (student == null) throw new Exception("Không tìm thấy học viên.");

                Console.WriteLine("Danh sách khóa học:");
                courses.ForEach(c => Console.WriteLine($"{c.CourseId}: {c.Name} ({c.Level})"));

                Console.Write("Mã khóa học: ");
                string? courseInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(courseInput)) throw new Exception("Mã khóa học không được để trống.");
                string courseId = courseInput.Trim().ToUpper();

                var course = courses.Find(c => c.CourseId.ToUpper() == courseId);
                if (course == null) throw new Exception("Không tìm thấy khóa học.");

                student.RegisterCourse(course);
                Console.WriteLine("✅ Đăng ký thành công!");
                break;

            case "3":
                Console.Write("Email học viên: ");
                string? examEmailInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(examEmailInput)) throw new Exception("Email không hợp lệ.");
                string examEmail = examEmailInput.Trim().ToLower();

                var st = students.Find(s => s.Email.ToLower() == examEmail);
                if (st == null) throw new Exception("Không tìm thấy học viên.");

                Console.Write("Mã khóa học: ");
                string? examCourseInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(examCourseInput)) throw new Exception("Mã khóa học không hợp lệ.");
                string examCourseId = examCourseInput.Trim().ToUpper();

                var cs = courses.Find(c => c.CourseId.ToUpper() == examCourseId);
                if (cs == null) throw new Exception("Không tìm thấy khóa học.");

                Console.Write("Điểm: ");
                string? scoreInput = Console.ReadLine();
                if (!double.TryParse(scoreInput, out double score))
                    throw new FormatException("Điểm không hợp lệ.");

                st.TakeExam(cs, score);
                Console.WriteLine("✅ Đã nhập điểm.");
                break;

            case "4":
                foreach (var s in students)
                {
                    s.DisplayInfo();
                    Console.WriteLine("----------");
                }
                break;

            case "5":
                DataManager.SaveToFile(students, "students.csv");
                Console.WriteLine("✅ Đã lưu file.");
                break;

            case "6":
                var loaded = DataManager.LoadFromFile("students.csv");
                if (loaded != null)
                {
                    students = loaded;
                    Console.WriteLine("✅ Đã đọc file.");
                }
                else
                {
                    Console.WriteLine("⚠️ Không thể đọc file.");
                }
                break;

            case "0":
                return;

            default:
                Console.WriteLine("⚠️ Lựa chọn không hợp lệ.");
                break;
        }
    }
    catch (FormatException fe)
    {
        Console.WriteLine($"⚠️ Lỗi định dạng: {fe.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️ Lỗi: {ex.Message}");
    }
}
