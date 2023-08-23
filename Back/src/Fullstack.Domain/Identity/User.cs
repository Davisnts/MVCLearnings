using Fullstack.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace Fullstack.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public Titulo Titulo { get; set; }
        public string Descricao { get; set; }
        public Funcao Funcao { get; set; }
        public string ImagemUrl { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }

    }
}