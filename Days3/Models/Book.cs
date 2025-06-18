namespace LibraryBookManagement.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public Book(int id, string title, string author, string genre, int quantity)
        {
            ID = id;
            Title = title;
            Author = author;
            Genre = genre;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"[{ID}] {Title} | {Author} | {Genre} | Số lượng: {Quantity}";
        }
    }
}
