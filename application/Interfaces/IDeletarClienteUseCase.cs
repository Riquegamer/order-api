
namespace application.Interfaces
{
    public interface IDeletarClienteUseCase
    {
        Task<bool> ExecuteAsync(Guid id);
    }
}
