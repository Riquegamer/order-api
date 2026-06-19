
using application.DTOs.Entrada;
using application.DTOs.Saida;

namespace application.Interfaces
{
    public interface ILoginUseCase
    {
        Task<LoginResponse?> ExecuteAsync(LoginRequest req);
    }
}
