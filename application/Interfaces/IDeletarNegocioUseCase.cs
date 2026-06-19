using application.DTOs.Entrada;

namespace application.Interfaces
{
    public interface IDeletarNegocioUseCase
    {
        Task<bool> ExecuteAsync(Guid id, DeletarNegocioRequest req);
    }
}
