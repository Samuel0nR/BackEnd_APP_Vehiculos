using API_VehiclesAPP.Entities;

namespace API_VehiclesAPP.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string ID, string Email, string Role, string Auth);
    }
}
