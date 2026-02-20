using FastFoodWeb.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace FastFoodWeb.Services
{
    public class ConfiguracionService
    {
        private readonly string _connectionString;

        public ConfiguracionService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }

        // --- MÉTODOS PARA CAMPOS DINÁMICOS ---


        public async Task<ConfiguracionApariencia> ObtenerColores()
        {
            var config = new ConfiguracionApariencia();
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                // Siempre traemos el primer registro o valores por defecto
                var cmd = new SqlCommand("SELECT TOP 1 Id, ColorPrimario, ColorSecundario FROM ConfiguracionApariencia", conn);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        config.Id = (int)reader["Id"];
                        config.ColorPrimario = reader["ColorPrimario"].ToString();
                        config.ColorSecundario = reader["ColorSecundario"].ToString();

                    }
                }
            }
            return config;
        }

        public async Task GuardarColores(ConfiguracionApariencia config)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                // Actualizamos siempre el registro existente o insertamos si está vacío
                string query = @"
                    MERGE ConfiguracionApariencia AS target
                    USING (SELECT @Id AS Id) AS source
                    ON (target.Id = source.Id)
                    WHEN MATCHED THEN
                        UPDATE SET ColorPrimario = @C1, ColorSecundario = @C2,  UltimaModificacion = GETDATE()
                    WHEN NOT MATCHED THEN
                        INSERT (ColorPrimario, ColorSecundario) VALUES (@C1, @C2);";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", config.Id == 0 ? 1 : config.Id);
                cmd.Parameters.AddWithValue("@C1", config.ColorPrimario);
                cmd.Parameters.AddWithValue("@C2", config.ColorSecundario);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}