using Fullstack.Domain.Identity;

namespace Fullstack.Domain
{
    public class Palestrante
    {
    public int Id { get; set; }
    public string MiniCurriculo { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public User User {get; set;}
    public IEnumerable<RedeSocial> RedeSociais { get; set; }
    public IEnumerable<PalestranteEvento> PalestranteEventos { get; set; }
}
}