namespace Models
{
    public class Enrollment
    {
        public Course Course { get; set; }
        public double Score { get; set; }

        public Enrollment(Course course, double score = 0)
        {
            Course = course;
            Score = score;
        }
    }
}
