using API_VehiclesAPP.Data;
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


        public async Task<RegisterResponseDTO> Register(RegisterRequestDTO registerRequest)
        {
            RegisterResponseDTO responseDTO = new();
            try
            {
                if (string.IsNullOrWhiteSpace(registerRequest.Email))
                    throw new Exception("Email requerido");

                if (string.IsNullOrWhiteSpace(registerRequest.Password))
                    throw new Exception("Password requerido");

                bool emailExists = await _dBContext.Users
                    .AnyAsync(u => u.Email == registerRequest.Email);

                if (emailExists)
                    throw new Exception("Email ya registrado");

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

                User user = new()
                {
                    Email = registerRequest.Email,
                    PasswordHash = passwordHash,
                    Role = "User",
                    IsActive = true, // Dsps Agregar Validador mediante Correo
                    CreatedAt = DateTime.Now,
                };

                _dBContext.Users.Add(user);
                await _dBContext.SaveChangesAsync();
            
                responseDTO = new RegisterResponseDTO
                {
                    UserId = user.UserId,
                    Email = user.Email,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al Registrar Usuario - {registerRequest} | {ex.Message}");
                throw;
            }

            return responseDTO;
        }
        
        public async Task<LoginResponseDTO> CompleteDataUser(Cliente client)
        {
            LoginResponseDTO loginResponse = new()
            {
                User = new()
            };

            try
            {
                // Validaciones
                var user = _dBContext.Users.Where(i => i.UserId == client.UserId).ToList();
                if (user.Count == 0 || user[0].UserId.ToString() != client.UserId.ToString() )
                    throw new Exception("ID de Usuario Inválido");

                bool clientExist = await _dBContext.Clientes.AnyAsync(i => i.UserId == client.UserId);
                if (clientExist)
                    throw new Exception("ID de Usuario Ya Existente");


                // Insert
                client.ClienteId = Guid.NewGuid();
                _ = _dBContext.Clientes.Add(client);
                await _dBContext.SaveChangesAsync();

                // Construccion de Response: Generar Token + DTO
                loginResponse.Token = _jwtService.GenerateToken(client.UserId.ToString(), user[0].Email, user[0].Role);

                loginResponse.IsAuth = true;
                loginResponse.User = user[0];

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al Completar Datos de Usuario | {ex.Message}");
                throw;
            }

            return loginResponse;

        }
        


        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest)
        {
            LoginResponseDTO loginResponse = new()
            {
                User = new()
            };

            try
            {
                // Validar que Reuqest no venga Null
                if (loginRequest == null) return loginResponse;

                // Buscar en DB Usuario
                var user = _dBContext.Users.FirstOrDefault(i => i.Email == loginRequest.Email);
                if (user == null) return loginResponse;

                // Validar Contraseña Ingresada
                bool isValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user!.PasswordHash);


                //Crear JWT
                var token = _jwtService.GenerateToken(user.UserId.ToString(), user.Email, user.Role);

                loginResponse.User = user;
                loginResponse.Token = token;
                loginResponse.IsAuth = isValid;
                                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al Iniciar Sesion {loginRequest} | {ex.Message}");
                throw;
            }

            return loginResponse;
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
