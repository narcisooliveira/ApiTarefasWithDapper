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
                var connection = await getConnectionAsync();
                var tarefa = await connection.QueryFirstOrDefaultAsync<Tarefa>("SELECT * FROM Tarefas WHERE Id = @Id", new { Id = id });
                connection.Close();
                return tarefa is null ? Results.NotFound() : Results.Ok(tarefa);
            });

            app.MapPost("/tarefas", async (GetConnectionAsync getConnectionAsync, Tarefa tarefa) =>
            {
                var connection = await getConnectionAsync();
                await connection.InsertAsync(tarefa);
                connection.Close();
                return Results.Created($"/tarefas/{tarefa.Id}", tarefa);
            });

            app.MapPut("/tarefas/{id:int}", async (GetConnectionAsync getConnectionAsync, int id, Tarefa tarefa) =>
            {
                var connection = await getConnectionAsync();
                var affectedRows = await connection.ExecuteAsync("UPDATE Tarefas SET Atividade = @Atividade, Status = @Status WHERE Id = @Id", new { Id = id, tarefa.Atividade, tarefa.Status });
                connection.Close();
                return affectedRows == 0 ? Results.NotFound() : Results.Ok();
            });

            app.MapDelete("/tarefas/{id:int}", async (GetConnectionAsync getConnectionAsync, int id) =>
            {
                var connection = await getConnectionAsync();
                var tarefa = connection.GetAsync<Tarefa>(id);
                await connection.DeleteAsync(tarefa);
                connection.Close();
                return tarefa is null ? Results.NotFound() : Results.Ok();
            });
        }
    }
}
