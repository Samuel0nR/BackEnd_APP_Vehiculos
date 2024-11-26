using api_dotNet_vehicles.Data;
using api_dotNet_vehicles.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace api_dotNet_vehicles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAngularApp")]
    public class VehiclesController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public VehiclesController(DBContext DbContext)
        {
            _dbContext = DbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetVehicles(int Categoria, string Marca)
        {

            switch (Categoria)
            {
                case 1:
                        var cars = await _dbContext.CarsDet
                        .Where(col => col.CodVehi == Categoria && col.Marca == Marca)
                        .ToListAsync();

                    return Ok(cars);
                        
                case 3:
                        var bikes = await _dbContext.BikesDet
                        .Where(col => col.CodVehi == Categoria && col.Marca == Marca)
                        .ToListAsync();
                    return Ok(bikes); 

                default:
                    return NotFound();
            }

        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TipoVModel>>> GetTypes(int Categoria)
        {
            if (Categoria <= 0)
            {
                return BadRequest("El parámetro 'Categoria' es requerido y debe ser mayor que 0.");
            }

            var data = await _dbContext.TipoVs
                .Where(col => col.CatV == Categoria)
                .Select(col => new TipoVModel
                {
                    Tipo = col.Tipo,
                })
                .ToListAsync();

            return data == null ? NotFound() : Ok(data);
        }

        //[HttpGet("Models")]
        //public void GetModelsBranch(string Marca)
        //{
        //    //var data = _dbContext.BikesDet.
        //    return;
        //}



        [HttpPost("Cars")]
        public async Task<ActionResult<IEnumerable<BikesDetModel>>> PostNewCar(CarsDetModel vehiculoModel)
        {
            _ = await _dbContext.CarsDet.AddAsync(vehiculoModel);
            _ = await _dbContext.SaveChangesAsync();

            return Ok();
        }
        
        [HttpPost("Bikes")]
        public async Task<ActionResult<IEnumerable<BikesDetModel>>> PostNewBike(BikesDetModel vehiculoModel)
        {
            _ = await _dbContext.BikesDet.AddAsync(vehiculoModel);
            _ = await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
