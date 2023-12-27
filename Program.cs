using ApiTarefasWithDapper.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddPersistence();
var app = builder.Build();


app.Run();
