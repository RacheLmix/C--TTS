namespace Models
{
    public class Person
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"👤 {FullName} | ✉ {Email}");
        }
    }
}
