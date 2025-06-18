using LibraryBookManagement.Models;
using LibraryBookManagement.Services;
using System;

namespace LibraryBookManagement
{
    class Program
    {
        static BookService bookService = new BookService();
        static QueueService queueService = new QueueService();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n📚 HỆ THỐNG QUẢN LÝ KHO SÁCH");
                Console.WriteLine("1. Thêm sách mới");
                Console.WriteLine("2. Tìm kiếm theo thể loại/tác giả");
                Console.WriteLine("3. Phân loại theo thể loại");
                Console.WriteLine("4. Mượn sách");
                Console.WriteLine("5. Trả sách");
                Console.WriteLine("6. Hàng chờ mượn sách");
                Console.WriteLine("7. Thống kê top 3 sách nhiều");
                Console.WriteLine("8. Liệt kê người đang mượn");
                Console.WriteLine("9. Thống kê thể loại");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn: ");
                var ch = Console.ReadLine();

                switch (ch)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        Console.Write("Nhập từ khóa: ");
                        bookService.SearchByAuthorOrGenre(Console.ReadLine());
                        break;
                    case "3":
                        bookService.GroupByGenre();
                        break;
                    case "4":
                        BorrowBook();
                        break;
                    case "5":
                        ReturnBook();
                        break;
                    case "6":
                        Console.Write("Nhập ID sách: ");
                        int bId = int.Parse(Console.ReadLine());
                        queueService.ProcessRequest(bId);
                        break;
                    case "7":
                        bookService.DisplayTopBooks();
                        break;
                    case "8":
                        bookService.ShowBorrowedBooks();
                        break;
                    case "9":
                        bookService.StatisticsGenres();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("❗ Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }

        static void AddBook()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Tên: ");
            string title = Console.ReadLine();
            Console.Write("Tác giả: ");
            string author = Console.ReadLine();
            Console.Write("Thể loại: ");
            string genre = Console.ReadLine();
            Console.Write("Số lượng: ");
            int quantity = int.Parse(Console.ReadLine());

            Book book = new Book(id, title, author, genre, quantity);
            bookService.AddBook(book);
        }

        static void BorrowBook()
        {
            Console.Write("Tên người mượn: ");
            string name = Console.ReadLine();
            Console.Write("ID sách: ");
            int id = int.Parse(Console.ReadLine());

            var book = LibraryBookManagement.Data.LibraryStorage.Books.Find(b => b.ID == id);
            if (book == null)
            {
                Console.WriteLine("❌ Không tìm thấy sách.");
                return;
            }

            if (book.Quantity > 0)
            {
                book.Quantity--;
                if (!LibraryBookManagement.Data.LibraryStorage.BorrowedBooksByUser.ContainsKey(name))
                    LibraryBookManagement.Data.LibraryStorage.BorrowedBooksByUser[name] = new List<int>();

                LibraryBookManagement.Data.LibraryStorage.BorrowedBooksByUser[name].Add(id);
                LibraryBookManagement.Data.LibraryStorage.BorrowHistory.Push($"Mượn: {id} bởi {name}");
                Console.WriteLine($"📗 {name} đã mượn sách ID {id}");
            }
            else
            {
                Console.WriteLine("⚠️ Sách đã hết. Bạn sẽ được đưa vào hàng chờ.");
                queueService.AddRequest(id, name);
            }
        }

        static void ReturnBook()
        {
            Console.Write("Tên người trả: ");
            string name = Console.ReadLine();
            Console.Write("ID sách: ");
            int id = int.Parse(Console.ReadLine());

            if (LibraryBookManagement.Data.LibraryStorage.BorrowedBooksByUser.ContainsKey(name) &&
                LibraryBookManagement.Data.LibraryStorage.BorrowedBooksByUser[name].Remove(id))
            {
                var book = LibraryBookManagement.Data.LibraryStorage.Books.Find(b => b.ID == id);
                if (book != null) book.Quantity++;

                LibraryBookManagement.Data.LibraryStorage.BorrowHistory.Push($"Trả: {id} bởi {name}");
                Console.WriteLine($"📘 {name} đã trả sách ID {id}");

                queueService.ProcessRequest(id); // Xử lý hàng chờ
            }
            else
            {
                Console.WriteLine("❌ Không tìm thấy thông tin mượn.");
            }
        }
    }
}
