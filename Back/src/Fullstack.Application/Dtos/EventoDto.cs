using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fullstack.Application.Dtos
{
    public class EventoDto
    {
    public int Id { get; set; }
    public string Local { get; set; }
    public string DataEvento { get; set; }
    public string Tema { get; set; }
    public int QtdPessoas { get; set; }
    public string ImagemUrl { get; set; }
    public int Telefone { get; set; }
    public string Email { get; set; } 
    public IEnumerable<LoteDto> Lotes { get; set; }
    public IEnumerable<RedeSocialDto> RedeSociais { get; set; }
    public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}