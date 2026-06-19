using application.DTOs.Saida;
using application.Interfaces;
using domain.Ports;

namespace application.UseCases
{
    public class ListarNegociosUseCase : IListarNegociosUseCase
    {
        private readonly INegocioRepository _negocioRepository;

        public ListarNegociosUseCase(INegocioRepository negocioRepository) 
        { 
            _negocioRepository = negocioRepository; 
        }

        public async Task<IEnumerable<NegocioResponse>> ExecuteAsync()
        {
            var negocios = await _negocioRepository.GetAllAsync();
            return negocios.Select(n => new NegocioResponse(n.Id, n.Nome, n.Email, n.Documento, n.Telefone));
        }
    }
}
