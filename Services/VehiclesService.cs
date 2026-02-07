using API_VehiclesAPP.Data;
using API_VehiclesAPP.Services.Interfaces;

namespace API_VehiclesAPP.Services
{
    public class VehiclesService(DBContext dBContext) : IVehiclesService
    {
        private DBContext _dbContext = dBContext;

        //public async Task<IEnumerable<TipoVModel>> GetTypes(int Categoria)
        //{
        //    try
        //    {
        //        if (Categoria <= 0)
        //        {
        //            return ("El parámetro 'Categoria' es requerido y debe ser mayor que 0.");
        //        }

        //        var data = await _dbContext.TipoVs
        //            .Where(col => col.CatV == Categoria)
        //            .ToListAsync();

        //        return data;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public async Task<ActionResult<IEnumerable<CarsDetModel>>> PostNewCar(CarsDetModel vehiculoModel)
        //{
        //    if (vehiculoModel == null)
        //    {
        //        return BadRequest(new { message = "El objeto CarsDetModel no puede ser nulo." });
        //    }

        //    try
        //    {
        //        var existingVehicle = await _dbContext.CarsDet
        //            .FirstOrDefaultAsync(v =>
        //                v.Modelo == vehiculoModel.Modelo);

        //        if (existingVehicle != null)
        //        {
        //            return Conflict(new { message = "Ya existe un vehículo con las mismas características en la base de datos." });
        //        }

        //        await _dbContext.CarsDet.AddAsync(vehiculoModel);
        //        await _dbContext.SaveChangesAsync();

        //        return Ok(new { message = "Vehículo agregado exitosamente." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Ocurrió un error al agregar el vehículo.", error = ex.Message });
        //        throw;
        //    }
        //}

        //public async Task<ActionResult<IEnumerable<BikesDetModel>>> PostNewBike(BikesDetModel vehiculoModel)
        //{
        //    if (vehiculoModel == null)
        //    {
        //        return BadRequest(new { message = "El objeto BikesDetModel no puede ser nulo." });
        //    }

        //    try
        //    {
        //        var existingVehicle = await _dbContext.BikesDet
        //            .FirstOrDefaultAsync(v =>
        //                v.Modelo == vehiculoModel.Modelo);

        //        if (existingVehicle != null)
        //        {
        //            return Conflict(new { message = "Ya existe un vehículo con las mismas características en la base de datos." });
        //        }

        //        await _dbContext.BikesDet.AddAsync(vehiculoModel);
        //        await _dbContext.SaveChangesAsync();

        //        return Ok(new { message = "Vehículo agregado exitosamente." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Ocurrió un error al agregar el vehículo.", error = ex.Message });
        //    }
        //}



    }
}
