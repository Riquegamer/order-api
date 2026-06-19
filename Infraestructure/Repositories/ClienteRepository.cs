using domain.Entities;
using domain.Ports;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        #region Constructor
            
        private readonly PostgreDbContext _context;
        public ClienteRepository(PostgreDbContext context)
        {
            _context = context;
        }
    
            #endregion
    
            #region Methods
    
        public async Task<ClienteEntity> CreateAsync(ClienteEntity cliente)
        {
            _context.cliente.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }    

        public async Task<IEnumerable<ClienteEntity>> GetAllAsync()
        {
            return await _context.cliente.ToListAsync();
        }

        public async Task<ClienteEntity> GetByIdAsync(Guid id)
        {
            return await _context.cliente.FindAsync(id);
        }

        public async Task<ClienteEntity> UpdateAsync(ClienteEntity cliente)
        {
            try
            {
                var atualizado = _context.cliente.Update(cliente);
                await _context.SaveChangesAsync();
                return atualizado.Entity;
            }
            catch
            {
                throw new Exception("Cliente não encontrado para atualização.");
            }
        }

            #endregion
    }
}
