using application.DTOs.Entrada;
using application.DTOs.Saida;
using application.Interfaces;
using domain.Ports;

namespace application.UseCases
{
    public class AtualizarNegocioUseCase : IAtualizarNegocioUseCase
    {
        #region Propriedades
        
        private readonly INegocioRepository _negocioRepository;

        #endregion

        #region Construtores

        public AtualizarNegocioUseCase(INegocioRepository negocioRepository)
        {
            _negocioRepository = negocioRepository;
        }

        #endregion

        #region Metodos
        
        public async Task<NegocioResponse?> ExecuteAsync(Guid id, AtualizarNegocioRequest request)
        {
            var negocio = await _negocioRepository.GetByIdAsync(id);
            if (negocio == null) return null;

            var EmailCheck = await _negocioRepository.GetByEmailAsync(request.Email);

            if (EmailCheck != null && EmailCheck.Email.Valor != negocio.Email.Valor)
            {
                throw new Exception($"Email {request.Email} já esta sendo usado por outro negócio");
            }

            negocio.Atualizar(request.Nome, request.Email, request.Documento, request.Telefone);

            await _negocioRepository.UpdateAsync(negocio);
            return new NegocioResponse
            (
                negocio.Id,
                negocio.Nome,
                negocio.Email,
                negocio.Documento.Valor,
                negocio.Telefone.Valor
            );
        }

        #endregion
    }
}
