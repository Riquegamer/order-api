using domain.Exceptions;
using domain.ValueObjects;

namespace domain.Entities
{
    public class NegocioEntity
    {
        #region Propriedades
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public string Documento { get; private set; }
        public string Telefone { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public DateTime AtualizadoEm { get; private set; }
        public DateTime? DeletadoEm { get; private set; }
        public string SenhaHash { get; private set; } = null!;

        #endregion

        #region Construtores
        private NegocioEntity() { }

        public NegocioEntity(string nome, string email, string documento, string telefone, string senhaHash)
        {
            if(string.IsNullOrWhiteSpace(nome)) throw new DomainException("Nome do negócio é obrigatório.");
            if (email == null) throw new DomainException("E-mail é obrigatório.");
            if (string.IsNullOrWhiteSpace(documento)) throw new DomainException("Documento do negócio é obrigatório.");
            if(string.IsNullOrWhiteSpace(telefone)) throw new DomainException("Telefone do negócio é obrigatório.");
            if (string.IsNullOrWhiteSpace(senhaHash)) throw new DomainException("Senha é obrigatória.");

            Id = new Guid();
            Nome = nome;
            Email = new Email(email);
            Documento = documento;
            Telefone = telefone;
            SenhaHash = senhaHash;
            CriadoEm = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
            DeletadoEm = null;
        }
        #endregion

        #region Metodos
        
        public void Atualizar(string nome, string email, string documento, string telefone)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new DomainException("Nome do negócio é obrigatório.");
            if (email == null) throw new DomainException("E-mail é obrigatório para atualização.");
            if (string.IsNullOrWhiteSpace(documento)) throw new DomainException("Documento do negócio é obrigatório.");
            if (string.IsNullOrWhiteSpace(telefone)) throw new DomainException("Telefone do negócio é obrigatório.");

            Nome = nome;
            Email = new Email(email);
            Documento = documento;
            Telefone = telefone;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Deletar()
        {
            DeletadoEm = DateTime.UtcNow;
        }

        #endregion

        // TODO: Criar VOs para telefone, email e documento
    }
}
