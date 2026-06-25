using application.DTOs.Entrada;
using application.DTOs.Saida;

namespace application.Interfaces
{
    public interface ICriarNegocioUseCase
    {
        Task<CreateNegocioResponse> ExecuteAsync(CreateNegocioRequest request);
    }
}
