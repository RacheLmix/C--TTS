using LibraryBookManagement.Data;
using System;
using System.Collections.Generic;

namespace LibraryBookManagement.Services
{
    public class QueueService
    {
        public void AddRequest(int bookId, string username)
        {
            if (!LibraryStorage.BookRequestQueues.ContainsKey(bookId))
                LibraryStorage.BookRequestQueues[bookId] = new Queue<string>();

            LibraryStorage.BookRequestQueues[bookId].Enqueue(username);
            Console.WriteLine($"📥 {username} đã vào hàng chờ mượn sách ID {bookId}");
        }

        public void ProcessRequest(int bookId)
        {
            if (LibraryStorage.BookRequestQueues.ContainsKey(bookId) &&
                LibraryStorage.BookRequestQueues[bookId].Count > 0)
            {
                var nextUser = LibraryStorage.BookRequestQueues[bookId].Dequeue();
                Console.WriteLine($"📤 {nextUser} được mượn sách ID {bookId}");

                if (!LibraryStorage.BorrowedBooksByUser.ContainsKey(nextUser))
                    LibraryStorage.BorrowedBooksByUser[nextUser] = new List<int>();

                LibraryStorage.BorrowedBooksByUser[nextUser].Add(bookId);
                LibraryStorage.BorrowHistory.Push($"Mượn (xếp hàng): {bookId} bởi {nextUser}");
            }
        }
    }
}
