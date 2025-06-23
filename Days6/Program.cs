using System;
using Data;
using Models;
using Services;
using Utils;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Khởi tạo DbContext và tạo CSDL nếu chưa có
using var context = new AppDbContext();
context.Database.EnsureCreated();

// 🔥 Seed dữ liệu nếu chưa có
SeedData.Initialize(context);

// Khởi tạo các service
var studentService = new StudentService(context);
var courseService = new CourseService(context);
var enrollmentService = new EnrollmentService(context);

// In tiêu đề
Console.WriteLine("🎓 Hệ thống Quản lý Học viên & Khóa học\n");

void ShowMenu()
{
    Console.WriteLine("\n===== MENU CRUD HỆ THỐNG =====");
    Console.WriteLine("1. Thêm học viên");
    Console.WriteLine("2. Sửa học viên");
    Console.WriteLine("3. Xóa học viên");
    Console.WriteLine("4. Thêm khóa học");
    Console.WriteLine("5. Sửa khóa học");
    Console.WriteLine("6. Xóa khóa học");
    Console.WriteLine("7. Đăng ký học viên vào khóa học");
    Console.WriteLine("8. Xem danh sách học viên");
    Console.WriteLine("9. Xem danh sách khóa học");
    Console.WriteLine("10. Xem danh sách học viên của một khóa học");
    Console.WriteLine("11. Xem danh sách khóa học có nhiều hơn 5 học viên");
    Console.WriteLine("12. Lọc học viên theo tên");
    Console.WriteLine("13. Lọc học viên theo ngày sinh");
    Console.WriteLine("14. Sắp xếp danh sách học viên theo họ tên tăng dần");
    Console.WriteLine("0. Thoát");
    Console.Write("Chọn chức năng: ");
}

while (true)
{
    ShowMenu();
    var choice = Console.ReadLine();
    if (choice == "0") break;
    switch (choice)
    {
        case "1":
            Console.Write("Họ tên: "); var name = Console.ReadLine();
            Console.Write("Email: "); var email = Console.ReadLine();
            Console.Write("Ngày sinh (yyyy-MM-dd): "); var dobStr = Console.ReadLine();
            DateTime dob; DateTime.TryParse(dobStr, out dob);
            studentService.AddStudent(new Student { FullName = name, Email = email, BirthDate = dob });
            Console.WriteLine("Đã thêm học viên!");
            break;
        case "2":
            Console.Write("ID học viên cần sửa: "); var idEdit = int.Parse(Console.ReadLine());
            var stEdit = studentService.GetStudentById(idEdit);
            if (stEdit == null) { Console.WriteLine("Không tìm thấy!"); break; }
            Console.Write($"Tên mới ({stEdit.FullName}): "); var newName = Console.ReadLine();
            Console.Write($"Email mới ({stEdit.Email}): "); var newEmail = Console.ReadLine();
            Console.Write($"Ngày sinh mới ({stEdit.BirthDate:yyyy-MM-dd}): "); var newDobStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) stEdit.FullName = newName;
            if (!string.IsNullOrWhiteSpace(newEmail)) stEdit.Email = newEmail;
            if (DateTime.TryParse(newDobStr, out var newDob)) stEdit.BirthDate = newDob;
            studentService.UpdateStudent(stEdit);
            Console.WriteLine("Đã cập nhật!");
            break;
        case "3":
            Console.Write("ID học viên cần xóa: "); var idDel = int.Parse(Console.ReadLine());
            studentService.DeleteStudent(idDel);
            Console.WriteLine("Đã xóa!");
            break;
        case "4":
            Console.Write("Tên khóa học: "); var cTitle = Console.ReadLine();
            Console.Write("Level (1-5): "); var cLevel = int.Parse(Console.ReadLine());
            Console.Write("Thời lượng (ngày): "); var cDuration = int.Parse(Console.ReadLine());
            courseService.AddCourse(new Course { Title = cTitle, Level = cLevel, Duration = cDuration });
            Console.WriteLine("Đã thêm khóa học!");
            break;
        case "5":
            Console.Write("ID khóa học cần sửa: "); var cIdEdit = int.Parse(Console.ReadLine());
            var cEdit = courseService.GetCourseById(cIdEdit);
            if (cEdit == null) { Console.WriteLine("Không tìm thấy!"); break; }
            Console.Write($"Tên mới ({cEdit.Title}): "); var cNewTitle = Console.ReadLine();
            Console.Write($"Level mới ({cEdit.Level}): "); var cNewLevelStr = Console.ReadLine();
            Console.Write($"Thời lượng mới ({cEdit.Duration}): "); var cNewDurationStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(cNewTitle)) cEdit.Title = cNewTitle;
            if (int.TryParse(cNewLevelStr, out var cNewLevel)) cEdit.Level = cNewLevel;
            if (int.TryParse(cNewDurationStr, out var cNewDuration)) cEdit.Duration = cNewDuration;
            courseService.UpdateCourse(cEdit);
            Console.WriteLine("Đã cập nhật!");
            break;
        case "6":
            Console.Write("ID khóa học cần xóa: "); var cIdDel = int.Parse(Console.ReadLine());
            courseService.DeleteCourse(cIdDel);
            Console.WriteLine("Đã xóa!");
            break;
        case "7":
            Console.Write("ID học viên: "); var sId = int.Parse(Console.ReadLine());
            Console.Write("ID khóa học: "); var coId = int.Parse(Console.ReadLine());
            enrollmentService.EnrollStudent(sId, coId);
            Console.WriteLine("Đã đăng ký!");
            break;
        case "8":
            Console.WriteLine("\nDanh sách học viên:");
            foreach (var s in studentService.GetAllStudents())
                Console.WriteLine($"- {s.Id}: {s.FullName} ({s.Email}), {s.BirthDate:yyyy-MM-dd}");
            break;
        case "9":
            Console.WriteLine("\nDanh sách khóa học:");
            foreach (var c in courseService.GetAllCourses())
                Console.WriteLine($"- {c.Id}: {c.Title} (Level {c.Level}, {c.Duration} ngày)");
            break;
        case "10":
            Console.Write("Nhập ID khóa học: ");
            if (int.TryParse(Console.ReadLine(), out var courseId))
            {
                var students = studentService.GetStudentsByCourse(courseId);
                if (students.Count == 0)
                {
                    Console.WriteLine("Không có học viên nào đăng ký khóa học này.");
                }
                else
                {
                    Console.WriteLine($"\nDanh sách học viên đã đăng ký khóa học {courseId}:");
                    foreach (var s in students)
                        Console.WriteLine($"- {s.Id}: {s.FullName} ({s.Email}), {s.BirthDate:yyyy-MM-dd}");
                }
            }
            else
            {
                Console.WriteLine("ID không hợp lệ!");
            }
            break;
        case "11":
            var courses = courseService.GetCoursesWithMoreThan5Students();
            if (courses.Count == 0)
                Console.WriteLine("Không có khóa học nào có hơn 5 học viên.");
            else
            {
                Console.WriteLine("\nKhóa học có hơn 5 học viên:");
                foreach (var c in courses)
                    Console.WriteLine($"- {c.Id}: {c.Title} (Level {c.Level}, {c.Duration} ngày)");
            }
            break;
        case "12":
            Console.Write("Nhập từ khóa tên học viên: ");
            var keyword = Console.ReadLine();
            var filteredByName = studentService.FilterStudentsByName(keyword);
            if (filteredByName.Count == 0)
                Console.WriteLine("Không tìm thấy học viên phù hợp.");
            else
                foreach (var s in filteredByName)
                    Console.WriteLine($"- {s.Id}: {s.FullName} ({s.Email}), {s.BirthDate:yyyy-MM-dd}");
            break;
        case "13":
            Console.Write("Từ ngày (yyyy-MM-dd, Enter để bỏ qua): ");
            var fromStr = Console.ReadLine();
            Console.Write("Đến ngày (yyyy-MM-dd, Enter để bỏ qua): ");
            var toStr = Console.ReadLine();
            DateTime? from = null, to = null;
            if (DateTime.TryParse(fromStr, out var f)) from = f;
            if (DateTime.TryParse(toStr, out var t)) to = t;
            var filteredByDate = studentService.FilterStudentsByBirthDate(from, to);
            if (filteredByDate.Count == 0)
                Console.WriteLine("Không tìm thấy học viên phù hợp.");
            else
                foreach (var s in filteredByDate)
                    Console.WriteLine($"- {s.Id}: {s.FullName} ({s.Email}), {s.BirthDate:yyyy-MM-dd}");
            break;
        case "14":
            var sorted = studentService.GetStudentsSortedByName();
            foreach (var s in sorted)
                Console.WriteLine($"- {s.Id}: {s.FullName} ({s.Email}), {s.BirthDate:yyyy-MM-dd}");
            break;
        default:
            Console.WriteLine("Chức năng không hợp lệ!");
            break;
    }
}

Console.WriteLine("\nKết thúc chương trình!");
