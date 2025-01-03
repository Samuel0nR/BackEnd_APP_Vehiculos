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
    public class VehiclesDataController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public VehiclesDataController(DBContext DbContext)
        {
            _dbContext = DbContext;
        }

        /*------ Data Vehiculos ------*/
        [HttpGet("Brand")]
        public async Task<ActionResult> GetVehicles(int Categoria, string Marca)
        {
            try
            {
                switch (Categoria)
                {
                    case 1:
                        var cars = await _dbContext.CarsDet
                        .Where(col => col.Marca == Marca)
                        .ToListAsync();

                        return Ok(cars);

                    case 3:
                        var bikes = await _dbContext.BikesDet
                        .Where(col => col.Marca == Marca)
                        .ToListAsync();
                        return Ok(bikes);

                    default:
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }

        [HttpGet("Models")]
        public async Task<ActionResult> GetModelsBranch(int Categoria, string Modelo)
        {
            switch (Categoria)
            {
                case 1:
                    var cars = await _dbContext.CarsDet
                    .Where(col => EF.Functions.Like(col.Modelo, $"%{Modelo}%"))
                    .ToListAsync();

                    return Ok(cars);

                case 3:
                    var bikes = await _dbContext.BikesDet
                    .Where(col => EF.Functions.Like(col.Modelo, $"%{Modelo}%"))
                    .ToListAsync();
                    return Ok(bikes);

                default:
                    return NotFound();
            }
        }

    }
}
