namespace application.DTOs.Entrada
{
    public record AtualizarNegocioRequest(
        string Nome,
        string Email,
        string Documento,
        string Telefone
    );
}
