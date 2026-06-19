using application.DTOs.Entrada;
using application.DTOs.Saida;
using application.Interfaces;
using domain.Entities;
using domain.Ports;

namespace application.UseCases
{
    public class EncotrarNegocioPorIDUseCase : IEncontrarNegocioPorIDUseCase
    {
        private readonly INegocioRepository _negocioRepository; 
        public EncotrarNegocioPorIDUseCase(INegocioRepository negocioRepository)
        {
            _negocioRepository = negocioRepository;
        }

        public async Task<NegocioResponse?> ExecuteAsync(EncontrarNegocioRequest req)
        {
            var negocio = await _negocioRepository.GetByIdAsync(req.id);
            return negocio != null ? new NegocioResponse(negocio.Id, negocio.Nome, negocio.Email, negocio.Documento, negocio.Telefone) : null;
        }
    }
}
