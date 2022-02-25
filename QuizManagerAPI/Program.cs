using QuizManagerAPI.Data;
using MongoDB.Driver;

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Setting up to use config from the database settings container using dependency injection
builder.Services.Configure<QuizDatabaseSettings>(builder.Configuration.GetSection("QuizDatabaseSettings"));
builder.Services.AddSingleton<QuizzesService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost",
                                              "http://localhost:8080");
                          builder.AllowAnyOrigin();
                          builder.AllowAnyMethod();
                          builder.AllowAnyHeader();
                          //builder.AllowCredentials();
                      });
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Default endpoint for no query parameters
app.MapGet("/", () => "Welcome to the Quiz API!");

//Creates an endpoint for the GET all quizzes request
app.MapGet("/api/quiz", async (QuizzesService quizService) => await quizService.Get());


//Creates an endpoint for a single quiz via QuizID (The ID field I have specified)
app.MapGet("/api/quiz/{id}", async (QuizzesService quizService, int id) =>
{
    var quiz = await quizService.Get(id);
    return quiz is null ? Results.NotFound() : Results.Ok(quiz);
});

//Creates and endpoint to POST a new quiz to
app.MapPost("api/quiz", async (QuizzesService quizService, Quiz quiz) =>
{
    await quizService.Create(quiz);
    return Results.Ok();
});

//Creates an endpoint to PUT requests to and make amendments to quizzes (based on QuizId field I created, not MongoDB Object ID)
app.MapPut("api/quiz/{id}", async (QuizzesService quizService, int id, Quiz updatedQuiz) =>
{
    var quiz = await quizService.Get(id);
    if (quiz is null) return Results.NotFound();

    updatedQuiz.id = quiz.id;
    await quizService.Update(id, updatedQuiz);

    return Results.NoContent();
});

//Creates an endpoint to DELETE requests from the database (based on QuizId field I created, not MongoDB Object ID)
app.MapDelete("api/quiz/{id}", async (QuizzesService quizService, int id) =>
{
    var quiz = await quizService.Get(id);
    if (quiz is null) return Results.NotFound();

    await quizService.Remove(quiz.id);

    return Results.NoContent();
});


app.Run();

