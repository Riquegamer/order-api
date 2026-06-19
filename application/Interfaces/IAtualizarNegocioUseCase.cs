using application.DTOs.Entrada;
using application.DTOs.Saida;

namespace application.Interfaces
{
    public interface IAtualizarNegocioUseCase
    {
        Task<NegocioResponse?> ExecuteAsync(Guid id, AtualizarNegocioRequest req);
    }
}
