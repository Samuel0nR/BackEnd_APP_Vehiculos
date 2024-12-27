using api_dotNet_vehicles.Data;
using api_dotNet_vehicles.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace api_dotNet_vehicles.Controllers
{
    [ApiController]
    [Route("api/Vehicles")]
    [EnableCors("AllowAngularApp")]
    public class VehiclesController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public VehiclesController(DBContext DbContext)
        {
            _dbContext = DbContext;
        }

        /*------ Info Categorías - Filtros - etc ------*/
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TipoVModel>>> GetTypes(int Categoria)
        {
            if (Categoria <= 0)
            {
                return BadRequest("El parámetro 'Categoria' es requerido y debe ser mayor que 0.");
            }

            var data = await _dbContext.TipoVs
                .Where(col => col.CatV == Categoria)
                //.Select(col => new TipoVModel
                //{
                //    Tipo = col.Tipo,
                //})
                .ToListAsync();

            return data == null ? NotFound() : Ok(data);
        }


        /*------ Insertar Nuevos Vehiculos ------*/
        [HttpPost("New_Car")]
        public async Task<ActionResult<IEnumerable<CarsDetModel>>> PostNewCar(CarsDetModel vehiculoModel)
        {
            if (vehiculoModel == null)
            {
                return BadRequest(new { message = "El objeto CarsDetModel no puede ser nulo." });
            }

            try
            {
                var existingVehicle = await _dbContext.CarsDet
                    .FirstOrDefaultAsync(v =>
                        v.Modelo == vehiculoModel.Modelo);

                if (existingVehicle != null)
                {
                    return Conflict(new { message = "Ya existe un vehículo con las mismas características en la base de datos." });
                }

                await _dbContext.CarsDet.AddAsync(vehiculoModel);
                await _dbContext.SaveChangesAsync();

                return Ok(new { message = "Vehículo agregado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al agregar el vehículo.", error = ex.Message });
            }
        }

        [HttpPost("New_Bike")]
        public async Task<ActionResult<IEnumerable<BikesDetModel>>> PostNewBike(BikesDetModel vehiculoModel)
        {
            if (vehiculoModel == null)
            {
                return BadRequest(new { message = "El objeto BikesDetModel no puede ser nulo." });
            }

            try
            {
                var existingVehicle = await _dbContext.BikesDet
                    .FirstOrDefaultAsync(v =>
                        v.Modelo == vehiculoModel.Modelo);

                if (existingVehicle != null)
                {
                    return Conflict(new { message = "Ya existe un vehículo con las mismas características en la base de datos." });
                }

                await _dbContext.BikesDet.AddAsync(vehiculoModel);
                await _dbContext.SaveChangesAsync();

                return Ok(new { message = "Vehículo agregado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al agregar el vehículo.", error = ex.Message });
            }
        }


        /*------ BD ------*/
        //[HttpGet("TestConnection")]
        //public async Task<ActionResult> TestConnection()
        //{
        //    try
        //    {
        //        await _dbContext.Database.CanConnectAsync();
        //        return Ok("Conexión exitosa a la base de datos.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al conectar a la base de datos: {ex.Message}");
        //    }
        //}
    }
}
