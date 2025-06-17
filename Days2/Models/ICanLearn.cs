namespace Models
{
    public interface ICanLearn
    {
        void RegisterCourse(Course course);
        void TakeExam(Course course, double score);
    }
}
