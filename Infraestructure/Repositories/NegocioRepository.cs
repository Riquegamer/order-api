using domain.Entities;
using domain.Ports;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class NegocioRepository : INegocioRepository
    {
        #region Constructor
        
        private readonly PostgreDbContext _context;
        public NegocioRepository(PostgreDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task<NegocioEntity> CreateAsync(NegocioEntity negocio)
        {
            _context.negocio.Add(negocio);
            await _context.SaveChangesAsync();
            return negocio;
        }

        public async Task<IEnumerable<NegocioEntity>> GetAllAsync() 
        {
            return await _context.negocio.ToListAsync();
        }

        public async Task<NegocioEntity> GetByIdAsync(Guid id)
        {
            return await _context.negocio.FindAsync(id);
        }

        public async Task<NegocioEntity?> GetByEmailAsync(string email)
        {
            return await _context.negocio.FirstOrDefaultAsync(n => n.Email == email);
        }

        public async Task<NegocioEntity> UpdateAsync(NegocioEntity negocio)
        {
            try{ 
            var atualizado = _context.negocio.Update(negocio);
            await _context.SaveChangesAsync();
            return atualizado.Entity;

        }catch
            { 
                throw new Exception("Negocio não encontrado para atualização.");
            }
        }
        #endregion
    }
}
