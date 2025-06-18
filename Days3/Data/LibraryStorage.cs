using LibraryBookManagement.Models;
using System.Collections.Generic;

namespace LibraryBookManagement.Data
{
    public static class LibraryStorage
    {
        public static List<Book> Books = new List<Book>();

        public static Stack<string> BorrowHistory = new Stack<string>();

        public static Dictionary<string, List<int>> BorrowedBooksByUser = new Dictionary<string, List<int>>();

        public static Dictionary<int, Queue<string>> BookRequestQueues = new Dictionary<int, Queue<string>>();

        public static HashSet<string> Genres = new HashSet<string>();
    }
}
