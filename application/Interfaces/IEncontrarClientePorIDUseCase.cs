using application.DTOs.Entrada;
using application.DTOs.Saida;

namespace application.Interfaces
{
    public interface IEncontrarClientePorIDUseCase
    {
        Task<ClienteResponse?> ExecuteAsync(EncontrarClienteRequest request);
    }
}
