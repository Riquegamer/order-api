using application.DTOs.Entrada;
using application.DTOs.Saida;
using application.Interfaces;
using domain.Ports;

namespace application.UseCases
{
    public class EncotrarNegocioPorIDUseCase : IEncontrarNegocioPorIDUseCase
    {
        #region Propriedades
        
        private readonly INegocioRepository _negocioRepository;
        
        #endregion
        
        #region Construtores
        
        public EncotrarNegocioPorIDUseCase(INegocioRepository negocioRepository)
        {
            _negocioRepository = negocioRepository;
        }
        
        #endregion
        
        #region Metodos
        
        public async Task<NegocioResponse?> ExecuteAsync(EncontrarNegocioRequest req)
        {
            var negocio = await _negocioRepository.GetByIdAsync(req.Id);
            return negocio != null ? new NegocioResponse(negocio.Id, negocio.Nome, negocio.Email, negocio.Documento, negocio.Telefone) : null;
        }

        #endregion
    }
}
