using MongoDB.Bson.Serialization.Attributes;

namespace QuizManagerAPI.Data
{
    //The below class is the data model for the application and is based on how the data is/how I want the data to be stored in MongoDB. 
    //The below is an example of a document in Mongo DB, which contains a variety of data types. 
    public class Quiz
    {
        //The below attribute tags are MongoDB specific and based on Bson (Binary JSON) which is what MongoDB uses. 
        //The two attribute tags below represent the Object ID field which is auto generated for each MongoDB document and is used to map it to this data model. 
        //[BsonId]
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        //public string Id { get; set; }

        //THe BSON element tag is used to map the data as it is laid out in MongoDB - Convention in .NET is to name fields in Pascal case and in Mongo it is convention to use Camel Case. The Bson element tags bind them to match. 
        [BsonElement("_id")]
        public int id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("questions")]
        public QuestionList[] Questions { get; set; }

        //The below is a complex object in MongoDB and is used to represent nested objects inside existing arrays. The below class determines the fields and format, the above line for code creates an array of this custom object.
        public class QuestionList
        {
            [BsonElement("question")]
            public string Question { get; set; }

            [BsonElement("answers")]
            public string[] Answers { get; set; }
        }
    }
}
