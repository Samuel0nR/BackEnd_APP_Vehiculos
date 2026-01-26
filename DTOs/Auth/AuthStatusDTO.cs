namespace API_VehiclesAPP.DTOs.Auth
{
    public class AuthStatusDTO
    {
        public bool IsAuth { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool ProfileCompleted { get; set; }
    }
}
