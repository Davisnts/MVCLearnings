using Fullstack.Domain.Identity;
using Fullstack.Persistence.Contexto;
using Fullstack.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Persistence
{
    public class UserPersistence : GeralPersistence, IUserPersist
    {
        private readonly FullstackContext _context;
        public UserPersistence(FullstackContext context) : base(context)
        {
           _context = context;
            
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
          return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
          return await _context.Users.SingleOrDefaultAsync(user => user.UserName == username.ToLower());
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
           return await _context.Users.ToListAsync();
        }


    }
}