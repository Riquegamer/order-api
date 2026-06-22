using application.DTOs.Entrada;
using application.DTOs.Saida;
using application.Interfaces;
using domain.Ports;

namespace application.UseCases
{
    public class EncontrarClientePorIDUseCase : IEncontrarClientePorIDUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public EncontrarClientePorIDUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

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
    }
}
