using application.DTOs.Entrada;
using application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace order_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        #region Propriedades
        
        private readonly ILoginUseCase _loginUseCase;
        
        #endregion
        
        #region Construtores
        
        public AuthController(ILoginUseCase loginUseCase) => _loginUseCase = loginUseCase;
        
        #endregion
        
        #region Endpoints
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _loginUseCase.ExecuteAsync(request);
            if (token == null) return Unauthorized(new {mensagem = "Credenciais inválidas." });
            return Ok(token);
        }
        
        #endregion
    }
}
