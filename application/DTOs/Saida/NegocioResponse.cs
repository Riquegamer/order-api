namespace application.DTOs.Saida
{
    public record NegocioResponse(Guid Id, string Nome, string Email, string Documento, string Telefone);
}
