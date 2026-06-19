using application.DTOs.Entrada;
using application.DTOs.Saida;
using application.Interfaces;
using domain.Entities;
using domain.Ports;

namespace application.UseCases
{
    public class CriarClienteUseCase : ICriarClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public CriarClienteUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteResponse> ExecuteAsync(CriarClienteRequest request)
        {
            var cliente = new ClienteEntity(request.nome, request.documento, request.nomeFantasia, request.telefone, request.email, request.negocioId);
            await _clienteRepository.CreateAsync(cliente);
            return new ClienteResponse(cliente.Id, cliente.Nome, cliente.NomeFantasia, cliente.Documento, cliente.Telefone, cliente.Email, cliente.NegocioId, cliente.CriadoEm, cliente.AtualizadoEm);
        }
    }
}
