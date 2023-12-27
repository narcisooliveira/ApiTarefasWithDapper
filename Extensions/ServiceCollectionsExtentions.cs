using System.Data.SqlClient;
using static ApiTarefasWithDapper.Data.TarefaContext;

namespace ApiTarefasWithDapper.Extensions
{
    public static class ServiceCollectionsExtentions
    {
        // Extension method used to add the persistence layer to the application.
        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Register the database connection as a service.
            builder.Services.AddScoped<GetConnectionAsync>(sp => async () =>
            {
                var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                return connection;
            });

            return builder;
        }
    }
}
