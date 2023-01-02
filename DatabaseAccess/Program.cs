using DatabaseAccess.FieldOfStudy;
using DatabaseAccess.Generic;
using DatabaseAccess.Question;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddScoped<GenericSql, GenericSql>();
builder.Services.AddScoped<AnswerOptionDb, AnswerOptionDb>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
