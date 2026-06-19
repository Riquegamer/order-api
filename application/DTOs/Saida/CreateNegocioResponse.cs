namespace application.DTOs.Saida
{
    public record CreateNegocioResponse(Guid Id, string Nome, string Email, string Documento, string Telefone);
}
// TODO CRIAR UMA UNICA RESPOSTA "UNIVERSAL" PARA A ENTIDADE DE NEGOCIO