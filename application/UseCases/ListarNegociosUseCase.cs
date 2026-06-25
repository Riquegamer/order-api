using application.DTOs.Saida;
using application.Interfaces;
using domain.Ports;

namespace application.UseCases
{
    public class ListarNegociosUseCase : IListarNegociosUseCase
    {
        #region Propriedades
        
        private readonly INegocioRepository _negocioRepository;
        
        #endregion
        
        #region Construtores
        
        public ListarNegociosUseCase(INegocioRepository negocioRepository) 
        { 
            _negocioRepository = negocioRepository; 
        }
        
        #endregion
        
        #region Metodos
        
        public async Task<IEnumerable<NegocioResponse>> ExecuteAsync()
        {
            var negocios = await _negocioRepository.GetAllAsync();
            return negocios.Select(n => new NegocioResponse(n.Id, n.Nome, n.Email, n.Documento, n.Telefone));
        }
        
        #endregion
    }
}
