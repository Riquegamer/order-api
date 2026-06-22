using application.DTOs.Entrada;
using application.DTOs.Saida;

namespace application.Interfaces
{
    public interface IAtualizarClienteUseCase
    {
        Task<ClienteResponse?> ExecuteAsync(Guid id, AtualizarClienteRequest request);
    }
}
