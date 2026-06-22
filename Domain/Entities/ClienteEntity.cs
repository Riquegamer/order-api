using domain.Exceptions;
using domain.ValueObjects;

namespace domain.Entities
{
    public class ClienteEntity
    {
        #region Propriedades
        public Guid Id { get; private set; }
        
        public string Documento { get; private set; }
        public string Nome { get; private set; }
        public string? NomeFantasia { get; private set; }
        public string Telefone { get; private set; }
        public Email Email { get; private set; }

        public Guid NegocioId { get; private set; }
        public virtual NegocioEntity Negocio { get; private set; } = null!;

        public DateTime CriadoEm { get; private set; }
        public DateTime AtualizadoEm { get; private set; }
        public DateTime? DeletadoEm { get; private set; }


        #endregion

        #region Construtores

        public ClienteEntity(string nome, string documento, string? nomeFantasia, string telefone, string email, Guid negocioId)
        {
            if(string.IsNullOrWhiteSpace(nome)) throw new DomainException("Nome do cliente é obrigatório.");
            if(string.IsNullOrWhiteSpace(documento)) throw new DomainException("Documento do cliente é obrigatório.");
            if(string.IsNullOrWhiteSpace(telefone)) throw new DomainException("Telefone do cliente é obrigatório.");
            if(string.IsNullOrWhiteSpace(email)) throw new DomainException("E-mail do cliente é obrigatório.");

            Id = Guid.NewGuid();
            Nome = nome;
            Documento = documento;
            NomeFantasia = nomeFantasia;
            Telefone = telefone;
            Email = email;
            NegocioId = negocioId;
            CriadoEm = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
        }

        protected ClienteEntity(){}

        #endregion

        #region Metodos

        public void Atualizar(string nome, string documento, string? nomeFantasia, string telefone, string email)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new DomainException("Nome do cliente é obrigatório.");
            if (string.IsNullOrWhiteSpace(documento)) throw new DomainException("Documento do cliente é obrigatório.");
            if (string.IsNullOrWhiteSpace(telefone)) throw new DomainException("Telefone do cliente é obrigatório.");
            if (string.IsNullOrWhiteSpace(email)) throw new DomainException("E-mail do cliente é obrigatório.");
            Nome = nome;
            Documento = documento;
            NomeFantasia = nomeFantasia;
            Telefone = telefone;
            Email = email;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Deletar()
        {
            DeletadoEm = DateTime.UtcNow;
        }

        #endregion
    }
}
