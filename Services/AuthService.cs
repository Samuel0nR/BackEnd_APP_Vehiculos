using System.Security.Claims;
using API_VehiclesAPP.Data;
using API_VehiclesAPP.DTOs;
using API_VehiclesAPP.DTOs.Auth;
using API_VehiclesAPP.Entities;
using API_VehiclesAPP.Services.Interfaces;

using BCrypt.Net;
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
                };

                _dBContext.Users.Add(user);
                await _dBContext.SaveChangesAsync();            

                loginResponseDTO = new()
                {
                    User = user,
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
                    .FirstOrDefaultAsync(u => u.UserId == userId);

                if (user == null)
                    throw new Exception("Usuario no encontrado.");

                bool clientExist = await _dBContext.Clientes
                    .AnyAsync(c => c.UserId == userId);

                if (clientExist)
                    throw new Exception("El usuario ya completó sus datos.");

                client.Rut = client.Rut.Replace(".", "").ToUpper();
                var newClient = new Cliente
                {
                    ClienteId = Guid.NewGuid(),
                    UserId = userId,
                    Nombre = client.Nombre,
                    Rut = client.Rut,
                    Telefono = client.Telefono,
                    Direccion = client.Direccion
                };

                _dBContext.Clientes.Add(newClient);
                await _dBContext.SaveChangesAsync();

                return new LoginResponseDTO
                {
                    User = user,
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
                User = new()
            };

            try
            {
                // Validar que Reuqest no venga Null
                if (loginRequest == null)
                    return loginResponse;

                // Buscar en DB Usuario
                var user = await _dBContext.Users.FirstOrDefaultAsync(i => i.Email == loginRequest.Email);
                if (user == null)
                    return loginResponse;

                // Validar Contraseña Ingresada
                bool isValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user!.PasswordHash);


                return isValid ?
                loginResponse = new LoginResponseDTO()
                {
                    User = new User()
                    {
                        Email = user.Email,
                        Username = user.Username,
                        Role = user.Role,
                        UserId = user.UserId,
                    },
                    Token = _jwtService.GenerateToken(user.UserId.ToString(), user.Email, user.Role, "true"),
                    IsAuth = isValid,
                    Code = "200",
                    Message = "OK",
                } 
                : 
                loginResponse = new LoginResponseDTO()
                {
                    User = new User(),
                    Token = null,
                    IsAuth = false,
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
            var userComplete = await _dBContext.Clientes.FirstOrDefaultAsync(c => c.UserId == userId);

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
