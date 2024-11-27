using CrudPrueba.Data;
using CrudPrueba.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CrudPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly StoreProcedureLogic _usuarioDataAccess;

        public UsuarioController(StoreProcedureLogic usuarioDataAccess)
        {
            _usuarioDataAccess = usuarioDataAccess;
        }
        //ver usuarios
        [HttpGet("ObtenerUsuarios")]
        public async Task<ActionResult> ObtenerUsuarios()
        {
            try
            {
                var parametros = new SqlParameter[]
                {
                };

                var dataTable = await _usuarioDataAccess.ExecuteProcedure("sp_ObtenerUsuarios", parametros);

                var usuarios = dataTable.AsEnumerable().Select(row => new
                {
                    Id = row.Field<int>("UsuarioID"),
                    Nombre = row.Field<string>("Nombre"),
                    Correo = row.Field<string>("Email")
                }).ToList();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        //Endpoint de estado de conexion 
        [HttpGet("ConexionDirectaSqlClient")]
            public string ProbarConexionSqlClient()
            {
                var connectionString = "Server=DESKTOP-594TH1B\\SQLEXPRESS;Database=PlataformaEvaluacionCursosDev4;Trusted_Connection=True;Encrypt=False;";
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        return "Conexión exitosa a la base de datos";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error al conectar: {ex.Message}";
                }
            }
        //Endpoint Crear
        [HttpPost("CrearUsuario")]
        public async Task<ActionResult> CrearUsuario([FromBody] Usuarios nuevoUsuario)
        {
            try
            {
                var parametros = new SqlParameter[]
                {
            new SqlParameter("@Nombre", nuevoUsuario.Nombre),
            new SqlParameter("@Correo", nuevoUsuario.Correo),
            new SqlParameter("@Contraseña", nuevoUsuario.Contraseña)
                };

                // Llamar al procedimiento almacenado para crear el usuario
                await _usuarioDataAccess.ExecuteProcedure("CrearUsuario", parametros);

                return Ok("Usuario creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear usuario: {ex.Message}");
            }
        }

        //Endpoint actualizar
        [HttpPut("ActualizarUsuario")]
        public async Task<ActionResult> ActualizarUsuario(string id, string nombre, string correo)
        {
            try
            {
                var parametros = new SqlParameter[]
                {
            new SqlParameter("@Id", id),
            new SqlParameter("@Nombre", nombre),
            new SqlParameter("@Correo", correo)
                };

                await _usuarioDataAccess.ExecuteProcedure("ActualizarUsuario", parametros);
                return Ok("Usuario actualizado");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar usuario: {ex.Message}");
            }
        }
        //Endpoint Eliminar
        [HttpDelete("EliminarUsuario")]
        public async Task<ActionResult> EliminarUsuario(string id)
        {
            try
            {
                var parametros = new SqlParameter[]
                {
            new SqlParameter("@Id", id)
                };

                await _usuarioDataAccess.ExecuteProcedure("EliminarUsuario", parametros);
                return Ok("Usuario eliminado");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar usuario: {ex.Message}");
            }
        }
    }
}
