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
            if(string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome do cliente é obrigatório.", nameof(nome));
            if(string.IsNullOrWhiteSpace(documento)) throw new ArgumentException("Documento do cliente é obrigatório.", nameof(documento));
            if(string.IsNullOrWhiteSpace(telefone)) throw new ArgumentException("Telefone do cliente é obrigatório.", nameof(telefone));
            if(string.IsNullOrWhiteSpace(email)) throw new ArgumentException("E-mail do cliente é obrigatório.", nameof(email));

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
        #endregion
    }
}
