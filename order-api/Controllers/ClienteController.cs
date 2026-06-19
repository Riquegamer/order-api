using application.DTOs.Entrada;
using application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace order_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        #region Fields
        private readonly ICriarClienteUseCase _criarClienteUseCase;
        private readonly IValidator<CriarClienteRequest> _CriarClienteValidator;
        private readonly IListarClientesUseCase _listarClientesUseCase;
        #endregion

        #region Constructor
        public ClienteController(ICriarClienteUseCase criarClienteUseCase, IValidator<CriarClienteRequest> criarClienteValidator, IListarClientesUseCase listarClientesUseCase)
        {
            _criarClienteUseCase = criarClienteUseCase;
            _CriarClienteValidator = criarClienteValidator;
            _listarClientesUseCase = listarClientesUseCase;
        }
        #endregion

        #region Endpoints
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] CriarClienteRequest req)
        {
            var validationResult = await _CriarClienteValidator.ValidateAsync(req);
            if(!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    mensagem = "Dados de cliente inválidos.",
                    erros = validationResult.Errors.Select(e => new { campo = e.PropertyName, erro = e.ErrorMessage })
                });
            }

            var result = await _criarClienteUseCase.ExecuteAsync(req);
            return StatusCode(201, result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListarClientes()
        {
            var result = await _listarClientesUseCase.ExecuteAsync();
            return Ok(result);
        }

        #endregion

    }
}
