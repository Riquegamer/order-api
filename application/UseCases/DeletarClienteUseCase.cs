using application.Interfaces;
using domain.Ports;

namespace application.UseCases
{
    public class DeletarClienteUseCase : IDeletarClienteUseCase
    {
        #region Propriedades
        
        private readonly IClienteRepository _clienteRepository;

        #endregion

        #region Construtores
        
        public DeletarClienteUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        #endregion

        #region Metodos

        public async Task<bool> ExecuteAsync(Guid id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null) return false;
            
            cliente.Deletar();

            var deletedCliente = await _clienteRepository.UpdateAsync(cliente);
            return true;
        }

        #endregion
    }
}
