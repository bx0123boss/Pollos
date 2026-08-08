using FastFoodWeb.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FastFoodWeb.Services
{
    public class ConfiguracionService
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration; // <-- Agregamos el campo privado

        public ConfiguracionService(IConfiguration configuration)
        {
            _configuration = configuration; // <-- Asignamos la instancia inyectada
            _connectionString = configuration.GetConnectionString("CadenaSQL")!;
        }

        // Modelo para transportar los datos del ticket
        public class DatosTicketConfig
        {
            public string[] Encabezados { get; set; } = Array.Empty<string>();
            public string[] PieDePagina { get; set; } = Array.Empty<string>();
            public string LogoPath { get; set; } = @"C:\Jaeger Soft\logo.jpg";
        }

        public async Task<DatosTicketConfig> ObtenerDatosTicketAsync()
        {
            var config = new DatosTicketConfig();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    string query = "SELECT TOP 1 DatosTicket, PieDeTicket, LogoPath FROM ConfiguracionEmpresa";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string datosRaw = reader["DatosTicket"]?.ToString() ?? "";
                                string pieRaw = reader["PieDeTicket"]?.ToString() ?? "";

                                config.Encabezados = datosRaw.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                config.PieDePagina = pieRaw.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                config.LogoPath = reader["LogoPath"]?.ToString() ?? config.LogoPath;

                                return config;
                            }
                        }
                    }
                }
            }
            catch
            {
                // Ahora _configuration sí existe en el ámbito de la clase
                config.Encabezados = _configuration.GetSection("TicketSettings:Encabezados").Get<string[]>() ?? new[] { "MI NEGOCIO" };
                config.PieDePagina = _configuration.GetSection("TicketSettings:Pie").Get<string[]>() ?? new[] { "¡Gracias por su compra!" };
                config.LogoPath = _configuration["TicketSettings:LogoPath"] ?? @"C:\Jaeger Soft\logo.jpg";
            }

            return config;
        }

        // --- MÉTODOS PARA CAMPOS DINÁMICOS ---

        public async Task<ConfiguracionApariencia> ObtenerColores()
        {
            var config = new ConfiguracionApariencia();
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
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
                string query = @"
                    MERGE ConfiguracionApariencia AS target
                    USING (SELECT @Id AS Id) AS source
                    ON (target.Id = source.Id)
                    WHEN MATCHED THEN
                        UPDATE SET ColorPrimario = @C1, ColorSecundario = @C2, UltimaModificacion = GETDATE()
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