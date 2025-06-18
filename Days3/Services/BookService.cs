using LibraryBookManagement.Data;
using LibraryBookManagement.Models;
using System;
using System.Linq;

namespace LibraryBookManagement.Services
{
    public class BookService
    {
        public void AddBook(Book book)
        {
            var existing = LibraryStorage.Books.FirstOrDefault(b => b.ID == book.ID);
            if (existing != null)
            {
                existing.Quantity += book.Quantity;
                Console.WriteLine($"✅ Đã tăng số lượng sách ID {book.ID} lên {existing.Quantity}");
            }
            else
            {
                LibraryStorage.Books.Add(book);
                LibraryStorage.Genres.Add(book.Genre);
                Console.WriteLine($"📚 Đã thêm sách mới: {book.Title}");
            }
        }

        public void SearchByAuthorOrGenre(string keyword)
        {
            var results = LibraryStorage.Books
                .Where(b => b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            b.Genre.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Any())
                results.ForEach(b => Console.WriteLine(b));
            else
                Console.WriteLine("❌ Không tìm thấy sách phù hợp.");
        }

        public void GroupByGenre()
        {
            var grouped = LibraryStorage.Books
                .GroupBy(b => b.Genre)
                .Select(g => new { Genre = g.Key, Total = g.Sum(b => b.Quantity) });

            foreach (var g in grouped)
                Console.WriteLine($"📖 {g.Genre}: {g.Total} quyển");
        }

        public void DisplayTopBooks()
        {
            var topBooks = LibraryStorage.Books
                .OrderByDescending(b => b.Quantity)
                .Take(3)
                .ToList();

            Console.WriteLine("🏆 Top 3 sách có số lượng nhiều nhất:");
            topBooks.ForEach(b => Console.WriteLine(b));
        }

        public void ShowBorrowedBooks()
        {
            var joined = from user in LibraryStorage.BorrowedBooksByUser
                         from bookId in user.Value
                         join book in LibraryStorage.Books on bookId equals book.ID
                         select new { User = user.Key, Book = book };

            Console.WriteLine("📋 Danh sách người đang mượn sách:");
            foreach (var item in joined)
                Console.WriteLine($"👤 {item.User} → {item.Book.Title}");
        }

        public void StatisticsGenres()
        {
            Console.WriteLine("📚 Thể loại hiện có:");
            foreach (var genre in LibraryStorage.Genres)
                Console.WriteLine($"- {genre}");
        }
    }
}
