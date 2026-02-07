using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api_dotNet_vehicles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAngularApp")]
    public class VehiclesDataController : ControllerBase
    {

        /*------ Data Vehiculos ------*/
        //[HttpGet("Brand")]
        //public async Task<ActionResult> GetVehicles(int Categoria, string Marca)
        //{
        //    try
        //    {
        //        switch (Categoria)
        //        {
        //            case 1:
        //                var cars = await _dbContext.CarsDet
        //                .Where(col => col.Marca == Marca)
        //                .ToListAsync();

        //                return Ok(cars);

        //            case 3:
        //                var bikes = await _dbContext.BikesDet
        //                .Where(col => col.Marca == Marca)
        //                .ToListAsync();
        //                return Ok(bikes);

        //            default:
        //                return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //        throw;
        //    }

        //}

        //[HttpGet("Models")]
        //public async Task<ActionResult> GetModelsBranch(int Categoria, string Modelo)
        //{
        //    switch (Categoria)
        //    {
        //        case 1:
        //            var cars = await _dbContext.CarsDet
        //            .Where(col => EF.Functions.Like(col.Modelo, $"%{Modelo}%"))
        //            .ToListAsync();

        //            return Ok(cars);

        //        case 3:
        //            var bikes = await _dbContext.BikesDet
        //            .Where(col => EF.Functions.Like(col.Modelo, $"%{Modelo}%"))
        //            .ToListAsync();
        //            return Ok(bikes);

        //        default:
        //            return NotFound();
        //    }
        //}

    }
}
