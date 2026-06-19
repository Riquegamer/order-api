namespace application.DTOs.Entrada
{
    public record CriarClienteRequest(string nome,string? nomeFantasia, string documento, string telefone, string email, Guid negocioId);
}
