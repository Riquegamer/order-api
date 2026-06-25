using application.DTOs.Entrada;
using application.DTOs.Saida;
using application.Interfaces;
using domain.Entities;
using domain.Ports;

namespace application.UseCases
{
    public class CriarClienteUseCase : ICriarClienteUseCase
    {
        #region Propriedades
        
        private readonly IClienteRepository _clienteRepository;

        #endregion
        
        #region Construtores
        
        public CriarClienteUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        #endregion

        #region Metodos

        public async Task<ClienteResponse> ExecuteAsync(CriarClienteRequest request)
        {
            var cliente = new ClienteEntity(request.Nome, request.Documento, request.NomeFantasia, request.Telefone, request.Email, request.NegocioId);
            await _clienteRepository.CreateAsync(cliente);
            return new ClienteResponse(cliente.Id, cliente.Nome, cliente.NomeFantasia, cliente.Documento, cliente.Telefone, cliente.Email, cliente.NegocioId, cliente.CriadoEm, cliente.AtualizadoEm);
        }

        #endregion
    }
}
