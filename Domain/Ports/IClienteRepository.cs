using domain.Entities;

namespace domain.Ports
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteEntity>> GetAllAsync();
        Task<ClienteEntity> GetByIdAsync(Guid id);
        Task <ClienteEntity> CreateAsync(ClienteEntity cliente);
        Task<ClienteEntity> UpdateAsync(ClienteEntity cliente);

    }
}
