using Microsoft.Data.SqlClient;
using System.Data;

namespace CrudPrueba.Data
{
    public class StoreProcedureLogic
    {
        private readonly string _connectionString;

        public StoreProcedureLogic(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Método para ejecutar un procedimiento almacenado
        public async Task<DataTable> ExecuteProcedure(string procedimiento, SqlParameter[] parametros = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(procedimiento, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parametros != null)
                    {
                        command.Parameters.AddRange(parametros);
                    }

                    var dataTable = new DataTable();

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar errores
                        throw new Exception($"Error al ejecutar el procedimiento almacenado: {ex.Message}", ex);
                    }

                    return dataTable;
                }
            }
        }
    }
}
