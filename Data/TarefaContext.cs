using System.Data;

namespace ApiTarefasWithDapper.Data;

public class TarefaContext
{
    public delegate Task<IDbConnection> GetConnectionAsync();
}