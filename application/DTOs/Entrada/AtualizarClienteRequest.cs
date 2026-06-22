namespace application.DTOs.Entrada
{
    public record AtualizarClienteRequest(string Nome, string Documento, string? NomeFantasia, string Telefone, string Email);
}
