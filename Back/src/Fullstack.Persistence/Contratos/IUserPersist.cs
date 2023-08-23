using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fullstack.Domain.Identity;

namespace Fullstack.Persistence.Contratos
{
    public interface IUserPersist : IGeralPersist
    {
        Task<IEnumerable<User>>  GetUsersAsync(); 
        Task<User> GetUserByIdAsync(int id); 
        Task<User> GetUserByUsernameAsync(string username);

        
    }
}