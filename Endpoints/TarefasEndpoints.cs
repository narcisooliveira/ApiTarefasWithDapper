using ApiTarefasWithDapper.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using static ApiTarefasWithDapper.Data.TarefaContext;

namespace ApiTarefasWithDapper.Endpoints
{
    public static class TarefasEndpoints
    {
        public static void MapTarefasEndpoints(this WebApplication app)
        {
            app.MapGet("/", () => $"Welcome! to API Tarefas - {DateTime.Now}");

            app.MapGet("/tarefas", async (GetConnectionAsync getConnectionAsync) =>
            {
                using var connection = await getConnectionAsync();
                var tarefas = await connection.GetAllAsync<Tarefa>();
                return Results.Ok(tarefas);
            });

            app.MapGet("/tarefas/{id:int}", async (GetConnectionAsync getConnectionAsync, int id) =>
            {
                using var connection = await getConnectionAsync();
                var tarefa = await connection.QueryFirstOrDefaultAsync<Tarefa>("SELECT * FROM Tarefa WHERE Id = @Id", new { Id = id });
                return tarefa is null ? Results.NotFound() : Results.Ok(tarefa);
            });

            app.MapPost("/tarefas", async (GetConnectionAsync getConnectionAsync, Tarefa tarefa) =>
            {
                using var connection = await getConnectionAsync();
                await connection.InsertAsync(tarefa);             
                return Results.Created($"/tarefas/{tarefa.Id}", tarefa);
            });

            app.MapPut("/tarefas/{id:int}", async (GetConnectionAsync getConnectionAsync, int id, Tarefa tarefa) =>
            {
                using var connection = await getConnectionAsync();
                var affectedRows = await connection.ExecuteAsync("UPDATE Tarefa SET Atividade = @Atividade, Status = @Status WHERE Id = @Id", new { Id = id, tarefa.Atividade, tarefa.Status });
                return affectedRows == 0 ? Results.NotFound() : Results.Ok();
            });

            app.MapDelete("/tarefas/{id:int}", async (GetConnectionAsync getConnectionAsync, int id) =>
            {
                using var connection = await getConnectionAsync();
                var affectedRows = await connection.ExecuteAsync("DELETE FROM Tarefa WHERE Id = @Id", new { Id = id });
                return affectedRows == 0 ? Results.NotFound() : Results.Ok();
            });
        }
    }
}
