namespace application.DTOs.Saida
{
    public record ClienteResponse(Guid id, string nome, string? nomeFantasia, string documento, string telefone, string email, Guid negocioId, DateTime criadoEm, DateTime atualizadoEm);
    // TODO: Verificar se é necessario retornar o negocio inteiro ou somente o id
}
