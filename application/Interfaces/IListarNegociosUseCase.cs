using application.DTOs.Saida;

namespace application.Interfaces
{
    public interface IListarNegociosUseCase
    {
        Task<IEnumerable<NegocioResponse>> ExecuteAsync();
    }
}
