using application.Interfaces;
using domain.Ports;

namespace application.UseCases
{
    public class DeletarClienteUseCase : IDeletarClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public DeletarClienteUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> ExecuteAsync(Guid id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null) return false;
            
            cliente.Deletar();

            var deletedCliente = await _clienteRepository.UpdateAsync(cliente);
            return true;
        }
    }
}
