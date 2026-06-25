using application.DTOs.Saida;
using application.Interfaces;
using domain.Ports;

namespace application.UseCases
{
    public class ListarClientesUseCase : IListarClientesUseCase
    {
        #region Propriedades
        
        private readonly IClienteRepository _clienteRepository;
        
        #endregion
        
        #region Construtores
        
        public ListarClientesUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        
        #endregion
        
        #region Metodos
        
        public async Task<IEnumerable<ClienteResponse>> ExecuteAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return clientes.Select(c => new ClienteResponse(c.Id, c.Nome, c.NomeFantasia, c.Documento, c.Telefone, c.Email, c.NegocioId, c.CriadoEm, c.AtualizadoEm));
        }
        
        #endregion
    }
}
