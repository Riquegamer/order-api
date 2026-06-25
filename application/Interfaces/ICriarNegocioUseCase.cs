using application.DTOs.Entrada;
using application.DTOs.Saida;

namespace application.Interfaces
{
    public interface ICriarNegocioUseCase
    {
        Task<NegocioResponse> ExecuteAsync(CreateNegocioRequest request);
    }
}
