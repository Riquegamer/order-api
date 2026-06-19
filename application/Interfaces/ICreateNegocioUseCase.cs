using application.DTOs.Entrada;
using application.DTOs.Saida;

namespace application.Interfaces
{
    public interface ICreateNegocioUseCase
    {
        Task<CreateNegocioResponse> ExecuteAsync(CreateNegocioRequest request);
    }
}
