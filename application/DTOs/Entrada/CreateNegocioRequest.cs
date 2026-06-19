namespace application.DTOs.Entrada
{
    public record CreateNegocioRequest(string Nome, string Email, string Documento, string Telefone, string Senha);
}
