using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace QuizManagerAPI.Data
{
    public class QuizzesService
    {
        //Used to allow operations to be performed against the database
        private readonly IMongoCollection<Quiz> _quizzes;
        public QuizzesService(IOptions<QuizDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);

            //Grabs the collection from the database using MongoClient class and stores in _quizzes 
            _quizzes = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<Quiz>(options.Value.QuizzesCollectionName);
        }

        //GET all quizzes from database
        public async Task<List<Quiz>> Get() => await _quizzes.Find(_ => true).ToListAsync();

        ////GET a single quiz from the database (based on ObjectId field)
        //public async Task<Quiz> Get(string id) => await _quizzes.Find(quiz => quiz.Id == id).FirstOrDefaultAsync();

        //GET a single quiz from the database (based on QuizID / quiz_id field)
        public async Task<Quiz> Get(int id) => await _quizzes.Find(quiz => quiz.id == id).FirstOrDefaultAsync();

        //POST request to add a new quiz to the database
        public async Task Create(Quiz newQuiz) => await _quizzes.InsertOneAsync(newQuiz);

        //PUT request to update a quiz (based on the QuizId field)
        public async Task Update(int id, Quiz updateQuiz) => await _quizzes.ReplaceOneAsync(quiz => quiz.id == id, updateQuiz);

        //DELETE request to renive a quiz (based on the QuizId field)
        public async Task Remove(int id) => await _quizzes.DeleteOneAsync(quiz => quiz.id == id);
    }
}
