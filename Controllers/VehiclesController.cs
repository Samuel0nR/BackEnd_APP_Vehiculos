using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API_VehiclesAPP.Controllers
{
    [ApiController]
    [Route("api/Vehicles")]
    [EnableCors("AllowAngularApp")]
    public class VehiclesController : ControllerBase
    {

        
        //[HttpGet("Types")]
        //public async Task<IActionResult> GetTypes(int Categoria)
        //{
        //    var data = "";

        //    return data == null ? NotFound() : Ok(data);
        //}


        /*------ Insertar Nuevos Vehiculos ------*/
        //[HttpPost("New_Car")]
        //public async Task<IActionResult> PostNewCar(CarsDetModel vehiculoModel)
        //{
        //    try
        //    {
        //        if (vehiculoModel == null)
        //        {
        //            return BadRequest(new { message = "El objeto CarsDetModel no puede ser nulo." });
        //        }

        //        var resp = "";
                
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Ocurrió un error al agregar el vehículo.", error = ex.Message });
        //    }
        //}

        //[HttpPost("New_Bike")]
        //public async Task<IActionResult> PostNewBike(BikesDetModel vehiculoModel)
        //{
        //    try
        //    {
        //        if (vehiculoModel == null)
        //        {
        //            return BadRequest(new { message = "El objeto BikesDetModel no puede ser nulo." });
        //        }
             
        //        var resp = "";

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Ocurrió un error al agregar el vehículo.", error = ex.Message });
        //    }
        //}


    }
}
