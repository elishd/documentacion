using AutoMapper;
using Dapper;
using Dapper.Oracle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using prueba_Api.Mapper;
using prueba_Api.Model;
using prueba_Api.Request;
using prueba_Api.Response;
using System.Configuration;
using System.Data;

namespace prueba_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        #region Constructores
        private IConfiguration _configuracion;
        private IMapper _mapper;

        public ProductosController(IConfiguration configuracion, IMapper mapper)
        {
            this._configuracion = configuracion;
            this._mapper = mapper;

        }

        #endregion

        #region Metodos
        [HttpGet]
        public async Task<IActionResult> GetAllCliente()
        {
            try
            {
                using var con = new OracleConnection(_configuracion["ConnectionStrings:Oracle"]);

                string query = @"SelectCliente";
                var parameters = new OracleDynamicParameters();
               
                parameters.Add("RESULTADO", dbType: (OracleMappingType?)OracleDbType.RefCursor, direction: ParameterDirection.Output);
                var dato = await con.QueryAsync<producto>(query, parameters, commandType: CommandType.StoredProcedure);
                var response = _mapper.Map<List<ProductoResponse>>(dato);

                return Ok(new GenericResponse(0, "OK", dato));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse(1, e.Message, null));
            }

        }

        [HttpPost]
        public async Task<IActionResult> SpCreateCliente(CreateProductoRequest data)
        {
            try
            {
                using var cn = new OracleConnection(_configuracion["ConnectionStrings:Oracle"]);

                var query = @"InsertarCliente ";

                var parameters = new DynamicParameters();
                parameters.Add("Nombre", data.Nombre);
                parameters.Add("Direccion", data.Direccion);
                parameters.Add("Salario", data.Salario);
                parameters.Add("Edad", data.Edad);
                parameters.Add("Codigo", dbType: DbType.Decimal, direction: ParameterDirection.Output);

                var dato = await cn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                decimal codigoGenerado = parameters.Get<decimal>("Codigo");

                return Ok(new GenericResponse(0, "OK", codigoGenerado));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse(1, e.Message, null));
            }
        }


        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetByCodigo( int codigo)
        {
            try
            {
                using var con = new OracleConnection(_configuracion["ConnectionStrings:Oracle"]);

                string query = "GETBYCODIGO";

                var parameters = new OracleDynamicParameters();
                parameters.Add("P_CODIGO", codigo);
                parameters.Add("LISTA", dbType: (OracleMappingType?)OracleDbType.RefCursor, direction: ParameterDirection.Output);


                var datos= await con.QueryAsync<ProductoResponse>(query, parameters, commandType: CommandType.StoredProcedure);
                //var repuesta =_mapper.Map<ProductoResponse>(datos);

                return Ok(new GenericResponse(0,"ok",datos));

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,new GenericResponse(1, e.Message, null));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCliente(int codigo)
        {
            try
            {
                using var cone = new OracleConnection(_configuracion["ConnectionStrings:Oracle"]);

                string query = "DELETECLIENTE";

                var parametros = new OracleDynamicParameters();
                parametros.Add("P_CODIGO",codigo);


                var datos= await cone.QueryAsync<ProductoResponse>(query,parametros, commandType: CommandType.StoredProcedure);

                return Ok( new GenericResponse(0,"OK",datos));
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,new GenericResponse(1,e.Message,null));
            }
        }
       
        #endregion
    }
}
