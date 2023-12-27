using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTarefasWithDapper.Data;

[Table("Tarefa")]
public record Tarefa(int Id, string Atividade, string Status);