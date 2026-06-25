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
        private readonly IValidator<CreateNegocioRequest> _createNegocioValidator;
        private readonly IAtualizarNegocioUseCase _updateNegocioUseCase;
        private readonly IValidator<AtualizarNegocioRequest> _updateValidator;
        private readonly IDeletarNegocioUseCase _deleteNegocioUseCase;

        #endregion

        #region Construtores

        public NegocioController(ICriarNegocioUseCase createNegocioUseCase, IValidator<CreateNegocioRequest> createNegocioValidator, IAtualizarNegocioUseCase updateNegocioUseCase, IValidator<AtualizarNegocioRequest> updateValidator, IDeletarNegocioUseCase deleteNegocioUseCase)
        {
            _createNegocioUseCase = createNegocioUseCase;
            _createNegocioValidator = createNegocioValidator;
            _updateNegocioUseCase = updateNegocioUseCase;
            _updateValidator = updateValidator;
            _deleteNegocioUseCase = deleteNegocioUseCase;
        }

        #endregion

        #region Endpoints

        [HttpPost]
        public async Task<IActionResult> CreateNegocio([FromBody] CreateNegocioRequest req)
        {
            try
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
            catch (Exception ex)
            {
                return Conflict (new { mensagem = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateNegocio(Guid id, [FromBody] AtualizarNegocioRequest req)
        {
            try
            {
                var validationResult = await _updateValidator.ValidateAsync(req);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new
                    {
                        mensagem = "Erros de validação.",
                        erros = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                    });
                }

                var result = await _updateNegocioUseCase.ExecuteAsync(id, req);

                if (result == null)
                {
                    return NotFound(new { mensagem = $"Negócio com o ID {id} não foi encontrado." });
                }

                return Ok(result);
            } catch (Exception ex) 
            {
                return Conflict(new { mensagem = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteNegocio(Guid id, [FromBody] DeletarNegocioRequest req)
        {
            var deletado = await _deleteNegocioUseCase.ExecuteAsync(id, req);
            if (!deletado)
                return NotFound(new { mensagem = $"Negócio com o ID {id} não foi encontrado." });

            return NoContent();
        }

        #endregion
    }
}
