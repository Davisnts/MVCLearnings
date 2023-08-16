namespace Fullstack.Domain;
public class Evento
{
    public int Id { get; set; }
    public string Local { get; set; }
    public DateTime? DataEvento { get; set; }
    public string Tema { get; set; }
    public int QtdPessoas { get; set; }
    public string ImagemUrl { get; set; }
    public int Telefone { get; set; }
    public string Email { get; set; } 
    public IEnumerable<Lote> Lotes { get; set; }
    public IEnumerable<RedeSocial> RedeSociais { get; set; }
    public IEnumerable<Palestrante> Palestrantes { get; set; }
    public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }
}
