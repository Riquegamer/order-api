using application.DTOs.Entrada;
using application.DTOs.Saida;
using application.Interfaces;
using domain.Ports;

namespace application.UseCases
{
    public class EncontrarClientePorIDUseCase : IEncontrarClientePorIDUseCase
    {
        #region Propriedades
        
        private readonly IClienteRepository _clienteRepository;

        #endregion
        
        #region Construtores
        
        public EncontrarClientePorIDUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        #endregion
        
        #region Metodos
        
        public async Task<ClienteResponse?> ExecuteAsync(EncontrarClienteRequest request)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.Id);

            if (cliente == null)
                return null;

            return new ClienteResponse
            (
                cliente.Id,
                cliente.Nome,
                cliente.NomeFantasia,
                cliente.Documento,
                cliente.Telefone,
                cliente.Email,
                cliente.NegocioId,
                cliente.CriadoEm,
                cliente.AtualizadoEm
            );
        }

        #endregion
    }
}
