using Fullstack.Domain;
using Fullstack.Persistence.Contexto;
using Fullstack.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Persistence
{
    public class GeralPersistence : IGeralPersist
    {
        private readonly FullstackContext _context;
        public GeralPersistence(FullstackContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}