namespace QuizManagerAPI.Data
{
    public class QuizDatabaseSettings
    {
        public string ConnectionString { get; set; } = String.Empty;

        public string DatabaseName { get; set; } = String.Empty;

        public string QuizzesCollectionName { get; set; } = String.Empty;
    }
}