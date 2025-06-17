namespace Models
{
    public class Course
    {
        public string CourseId { get; set; }
        public string Name { get; set; }
        public CourseLevel Level { get; set; }

        public Course(string id, string name, CourseLevel level)
        {
            CourseId = id;
            Name = name;
            Level = level;
        }
    }
}
