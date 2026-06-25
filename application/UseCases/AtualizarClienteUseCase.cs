using application.DTOs.Entrada;
using application.DTOs.Saida;
using application.Interfaces;
using domain.Ports;

namespace application.UseCases
{
    public class AtualizarClienteUseCase : IAtualizarClienteUseCase
    {
        #region Propriedades
        
        private readonly IClienteRepository _clienteRepository;

        #endregion

        #region Contrutores
        
        public AtualizarClienteUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        #endregion

        #region Metodos
        
        public async Task<ClienteResponse?> ExecuteAsync(Guid id, AtualizarClienteRequest request)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null) return null;

            cliente.Atualizar(request.Nome, request.Documento, request.NomeFantasia, request.Telefone, request.Email);
            
            await _clienteRepository.UpdateAsync(cliente);
            return new ClienteResponse
            (
                cliente.Id,
                cliente.Nome,
                cliente.NomeFantasia,
                cliente.Documento,
                cliente.Telefone,
                cliente.Email.Valor,
                cliente.NegocioId,
                cliente.CriadoEm,
                cliente.AtualizadoEm
            );
        }

        #endregion

    }
}
