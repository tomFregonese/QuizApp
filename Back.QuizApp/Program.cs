using Ynov.QuizApp.Controllers;
using Ynov.QuizApp.Controllers.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<CategoryMapper>();

builder.Services.AddScoped <IQuizService, QuizService>();
builder.Services.AddScoped<QuizMapper>();

builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<QuestionMapper>();
builder.Services.AddScoped<AnswerMapper>();

builder.Services.AddScoped <IUserService, UserService>();
builder.Services.AddScoped<UserMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
