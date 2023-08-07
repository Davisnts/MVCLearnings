using fullstack.API.Models;
using Microsoft.AspNetCore.Mvc;
namespace fullstack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
         
        public EventoController()
        { 
        }
        public IEnumerable<Evento> _evento = new Evento[]{
            new Evento() {
                EventoId = 1,
                Tema = "Angular and Dotnet",
                Local = "Tres Pontas",
                Lote = "1º Lote",
                QtdPessoas = 250,
                DataEvento = DateTime.Now.AddDays(2).ToString(),
                ImagemUrl ="oi.png"
            },
            new Evento() {
                EventoId = 2,
                Tema = "Angular and Dotnet",
                Local = "Tres Pontas",
                Lote = "1º Lote",
                QtdPessoas = 250,
                DataEvento = DateTime.Now.AddDays(2).ToString(),
                ImagemUrl ="oi.png"
            }
        };

           
        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _evento;
                
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> GetByGet(int id)
        {
            return _evento.Where(evento => evento.EventoId == id);
                
        }

        [HttpPost]
        public string Post()
        {
            return "Post";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Exemplo de put com id = {id}";
        }
    }
}
