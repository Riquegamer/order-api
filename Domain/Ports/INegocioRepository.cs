using domain.Entities;

namespace domain.Ports
{

    public interface INegocioRepository
    {
        Task<IEnumerable<NegocioEntity>> GetAllAsync();
        Task<NegocioEntity> GetByIdAsync(Guid id);
        Task<NegocioEntity?> GetByEmailAsync(string email);
        Task<NegocioEntity> CreateAsync(NegocioEntity negocio);
        Task<NegocioEntity> UpdateAsync(NegocioEntity negocio);
    }
}