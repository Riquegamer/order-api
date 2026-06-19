using application.DTOs.Saida;

namespace application.Interfaces
{
    public interface IListarClientesUseCase
    {
        Task<IEnumerable<ClienteResponse>> ExecuteAsync();
    }
}
