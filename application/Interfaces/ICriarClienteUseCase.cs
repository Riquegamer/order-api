using application.DTOs.Entrada;
using application.DTOs.Saida;

namespace application.Interfaces
{
    public interface ICriarClienteUseCase
    {
        Task<ClienteResponse> ExecuteAsync(CriarClienteRequest request);
    }
}
