using application.DTOs.Entrada;
using application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace order_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NegocioController : ControllerBase
    {
        #region Propriedades

        private readonly ICriarNegocioUseCase _createNegocioUseCase;
        private readonly IEncontrarNegocioPorIDUseCase _encontrarNegocioPorIDUseCase;
        private readonly IListarNegociosUseCase _listarNegociosUseCase;
        private readonly IValidator<CreateNegocioRequest> _createNegocioValidator;
        private readonly IAtualizarNegocioUseCase _updateNegocioUseCase;
        private readonly IValidator<AtualizarNegocioRequest> _updateValidator;
        private readonly IDeletarNegocioUseCase _deleteNegocioUseCase;

        #endregion

        #region Construtores

        public NegocioController(ICriarNegocioUseCase createNegocioUseCase, IValidator<CreateNegocioRequest> createNegocioValidator, IAtualizarNegocioUseCase updateNegocioUseCase, IValidator<AtualizarNegocioRequest> updateValidator, IEncontrarNegocioPorIDUseCase encontrarNegocioPorIDUseCase, IListarNegociosUseCase listarNegociosUseCase, IDeletarNegocioUseCase deleteNegocioUseCase)
        {
            _createNegocioUseCase = createNegocioUseCase;
            _createNegocioValidator = createNegocioValidator;
            _updateNegocioUseCase = updateNegocioUseCase;
            _updateValidator = updateValidator;
            _encontrarNegocioPorIDUseCase = encontrarNegocioPorIDUseCase;
            _listarNegociosUseCase = listarNegociosUseCase;
            _deleteNegocioUseCase = deleteNegocioUseCase; 
        }

        #endregion

        #region Endpoints

        [HttpPost]
        public async Task<IActionResult> CreateNegocio([FromBody] CreateNegocioRequest req)
        {
            var validationResult = await _createNegocioValidator.ValidateAsync(req);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    mensagem = "Erros de validação.",
                    erros = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                });
            }
                var result = await _createNegocioUseCase.ExecuteAsync(req);
                return StatusCode(201, result);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateNegocio(Guid id, [FromBody] AtualizarNegocioRequest req)
        {
            var validationResult = await _updateValidator.ValidateAsync(req);
            if(!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    mensagem = "Erros de validação.",
                    erros = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                });
            }

            var result = await _updateNegocioUseCase.ExecuteAsync(id, req);

            if(result == null)
            {
                return NotFound(new { mensagem = $"Negócio com o ID {id} não foi encontrado." });
            }
            
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllNegocios()
        {
            var result = await _listarNegociosUseCase.ExecuteAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetNegocioById(Guid id)
        {
            var result = await _encontrarNegocioPorIDUseCase.ExecuteAsync(new EncontrarNegocioRequest(id));
            if (result == null)
                return NotFound(new { mensagem = $"Negócio com o ID {id} não foi encontrado." });

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteNegocio(Guid id, [FromBody] DeletarNegocioRequest req)
        {
            var deletado = await _deleteNegocioUseCase.ExecuteAsync(id, req);
            if(!deletado)
                return NotFound(new { mensagem = $"Negócio com o ID {id} não foi encontrado." });
            
            return NoContent();
        }

        #endregion
    }
}
