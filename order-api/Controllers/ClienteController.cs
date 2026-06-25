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
        #region Propriedades

        private readonly ICriarClienteUseCase _criarClienteUseCase;
        private readonly IValidator<CriarClienteRequest> _CriarClienteValidator;
        private readonly IListarClientesUseCase _listarClientesUseCase;
        private readonly IAtualizarClienteUseCase _atualizarClienteUseCase;
        private readonly IValidator<AtualizarClienteRequest> _atualizarClienteValidator;
        private readonly IEncontrarClientePorIDUseCase EncontrarClienteUseCase;
        private readonly IDeletarClienteUseCase _deletarClienteUseCase;

        #endregion

        #region Construtores

        public ClienteController(ICriarClienteUseCase criarClienteUseCase, IValidator<CriarClienteRequest> criarClienteValidator, IListarClientesUseCase listarClientesUseCase, IEncontrarClientePorIDUseCase encontrarClienteUseCase, IAtualizarClienteUseCase atualizarClienteUseCase, IDeletarClienteUseCase deletarClienteUseCase, IValidator<AtualizarClienteRequest> atualizarClienteValidator)
        {
            _criarClienteUseCase = criarClienteUseCase;
            _CriarClienteValidator = criarClienteValidator;
            _listarClientesUseCase = listarClientesUseCase;
            EncontrarClienteUseCase = encontrarClienteUseCase;
            _atualizarClienteUseCase = atualizarClienteUseCase;
            _atualizarClienteValidator = atualizarClienteValidator;
            _deletarClienteUseCase = deletarClienteUseCase;

        }

        #endregion

        #region Endpoints

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] CriarClienteRequest req)
        {
            var validationResult = await _CriarClienteValidator.ValidateAsync(req);
            if (!validationResult.IsValid)
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
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> AtualizarCliente(Guid id, [FromBody] AtualizarClienteRequest req)
        {
            var validationResult = await _atualizarClienteValidator.ValidateAsync(req);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    mensagem = "Dados de cliente inválidos.",
                    erros = validationResult.Errors.Select(e => new { campo = e.PropertyName, erro = e.ErrorMessage })
                });
            }
            var result = await _atualizarClienteUseCase.ExecuteAsync(id, req);
            if (result == null)
            {
                return NotFound(new { mensagem = $"Cliente com id {id} não encontrado." });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListarClientes()
        {
            var result = await _listarClientesUseCase.ExecuteAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> EncontrarCliente(Guid id)
        {
            var result = await EncontrarClienteUseCase.ExecuteAsync(new EncontrarClienteRequest(id));
            if (result == null)
            {
                return NotFound(new { mensagem = $"Cliente com id {id} não encontrado." });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> ExcluirCliente(Guid id)
        {
            var result = await _deletarClienteUseCase.ExecuteAsync(id);
            if (!result)
            {
                return NotFound(new { mensagem = $"Cliente com id {id} não encontrado." });
            }
            return Ok(result);
        }

        #endregion

    }
}