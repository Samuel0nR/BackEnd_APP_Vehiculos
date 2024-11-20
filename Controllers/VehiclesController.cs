using api_dotNet_vehicles.Data;
using api_dotNet_vehicles.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<VehiculoModel>>> GetVehicles(string Marca)
        {
            var data = await _dbContext.Vehiculos
                .Where(col => col.Marca == Marca)
                .ToListAsync();

            return data == null ? NotFound() : Ok(data);
        }

        [HttpGet("Categories")]
        public async Task<ActionResult<IEnumerable<CategoriaVModel>>> GetCategories()
        {
            return await _dbContext.CategoriaVs.ToListAsync();
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

        [HttpGet("Models")]
        public void GetModelsBranch(string Marca)
        {
            //var data = _dbContext.Vehiculos.
            return;
        }

    }
}
