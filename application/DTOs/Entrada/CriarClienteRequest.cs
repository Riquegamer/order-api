namespace application.DTOs.Entrada
{
    public record CriarClienteRequest(string Nome,string? NomeFantasia, string Documento, string Telefone, string Email, Guid NegocioId);
}
