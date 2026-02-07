using API_VehiclesAPP.Data;
using API_VehiclesAPP.DTOs;
using API_VehiclesAPP.DTOs.Auth;
using API_VehiclesAPP.Entities;
using API_VehiclesAPP.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_VehiclesAPP.Services
{
    public class AuthService(DBContext dBContext, ILogger<AuthService> logger, IJwtService jwtService) : IAuthService
    {
        private readonly DBContext _dBContext = dBContext;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly IJwtService _jwtService = jwtService;


        public async Task<LoginResponseDTO> Register(RegisterRequestDTO registerRequest)
        {
            RegisterResponseDTO responseDTO = new();
            LoginResponseDTO loginResponseDTO = null!;

            try
            {
                if (string.IsNullOrWhiteSpace(registerRequest.Email))
                    throw new Exception("Email Requerido");

                if (string.IsNullOrWhiteSpace(registerRequest.Password))
                    throw new Exception("Password Requerido");

                bool emailExists = await _dBContext.Users
                    .AnyAsync(u => u.Email == registerRequest.Email);

                if (emailExists)
                    throw new ArgumentException("Email Registrado, Intente con Otro.");

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

                User user = new()
                {
                    Email = registerRequest.Email,
                    PasswordHash = passwordHash,
                    Role = "User",
                    IsActive = true, // Dsps Agregar Validador mediante Correo
                    CreatedAt = DateTime.UtcNow,
                    Username = null,
                    Clients = new(),
                };

                _dBContext.Users.Add(user);
                await _dBContext.SaveChangesAsync();            

                loginResponseDTO = new()
                {
                    User = new()
                    {
                        Email = registerRequest.Email,
                        UserId = user.UserId,
                    },
                    IsAuth = false,
                    Token = _jwtService.GenerateToken(user.UserId.ToString(), user.Email, user.Role, "false"),
                };

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al Registrar Usuario - {registerRequest} | {ex.Message}");
                throw;
            }

            return loginResponseDTO;
        }
        public async Task<LoginResponseDTO> CompleteDataUser(Guid userId, ClientDTO client)
        {
            try
            {
                var user = await _dBContext.Users
                    .FirstOrDefaultAsync(u => u.UserId == userId) ?? throw new Exception("Usuario no encontrado.");

                var clientExist = await _dBContext.Clients
                    .FirstOrDefaultAsync(c => c.UserId == userId) ?? throw new Exception("El usuario ya completó sus datos.");


                client.Rut = client.Rut.Replace(".", "").ToUpper();
                var newClient = new Client
                {
                    ClienteId = Guid.NewGuid(),
                    UserId = userId,
                    Nombre = client.Nombre,
                    Rut = client.Rut,
                    Telefono = client.Telefono,
                    Direccion = client.Direccion
                };

                _dBContext.Clients.Add(newClient);
                await _dBContext.SaveChangesAsync();

                return new LoginResponseDTO
                {
                    User = new()
                    {
                        UserId = userId,
                        Email = client.Email,
                    },
                    Token = _jwtService.GenerateToken(
                        userId.ToString(),
                        user.Email,
                        user.Role,
                        "true"
                    ),
                    IsAuth = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al completar datos del usuario");
                throw;
            }
        }


        public async Task<LoginResponseDTO?> Login(LoginRequestDTO loginRequest)
        {
            LoginResponseDTO? loginResponse = new()
            {
                User = new() { Email = string.Empty, UserId = Guid.Empty}
            };

            try
            {
                // Validar que Request no venga Null
                if (loginRequest == null)
                    return loginResponse;

                // Buscar en DB Usuario
                var user = await _dBContext.Users.FirstOrDefaultAsync(i => i.Email == loginRequest.Email);
                if (user == null)
                    return loginResponse;

                // Validar Contraseña Ingresada
                bool isValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user!.PasswordHash);

                // Validar Info Cliente
                var hasProfile = await _dBContext.Clients
                    .AnyAsync(c => c.UserId == user.UserId);


                return isValid ?
                loginResponse = new LoginResponseDTO()
                {
                    User = new ClientDTO()
                    {
                        Nombre = $"{user.Clients?.Nombre ?? "[Nombre]"} {user.Clients?.Apellido ?? "[Apellido]"}",
                        Email = user.Email,
                        Username = user.Username ?? "",
                        Role = user.Role,
                        UserId = user.UserId,
                    },
                    Token = _jwtService.GenerateToken(user.UserId.ToString(), user.Email, user.Role, "true"),
                    IsAuth = isValid,
                    ProfileComplete = hasProfile,
                    Code = "200",
                    Message = "OK",
                } 
                : 
                loginResponse = new LoginResponseDTO()
                {
                    User = new() { Email = string.Empty, UserId = Guid.Empty },
                    Token = null,
                    IsAuth = false,
                    ProfileComplete = false,
                    Code = "400",
                    Message = "Contraseña Incorrecta.",
                };

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al Iniciar Sesion {loginRequest} | {ex.Message}");
                throw;
            }
        }


        public async Task<AuthStatusDTO> CheckStatus(Guid userId)
        {
            //Buscar Usuario
            var user = await _dBContext.Users.FirstOrDefaultAsync(u => u.UserId == userId); 

            //Validar si Existe en tabla Clientes (Datos del Usuario)
            var userComplete = await _dBContext.Clients.FirstOrDefaultAsync(c => c.UserId == userId);

            return user == null ? 
                throw new UnauthorizedAccessException()
                : 
                new AuthStatusDTO
                {
                    IsAuth = true,
                    Email = user.Email,
                    Role = user.Role,
                    ProfileCompleted = userComplete != null,
                };
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
