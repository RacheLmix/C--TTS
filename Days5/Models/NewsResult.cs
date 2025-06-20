namespace Models
{
    public class NewsResult
    {
        public string Source { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public bool IsSuccess => !string.IsNullOrEmpty(Content);
    }
}
