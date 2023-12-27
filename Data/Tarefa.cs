using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTarefasWithDapper.Data;

[Table("Tarefas")]
public record Tarefa(int Id, string Atividade, string Status);