using Fullstack.Domain;
namespace Fullstack.Persistence.Contratos
{
    public interface ILotePersist
    {
        //EVENTO
        /// <summary>
        /// Metodos get que retornará apenas 1 lote
        /// </summary>
        /// <param name="eventoId">Chave estrangeira que referencia o campo id da tabela evento</param>
        /// <returns>Retorna todos os lotes cujo o Eventoid seja relaciondo ao lote </returns>
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
        /// <summary>
        /// Metodos get que retornará apenas 1 lote
        /// </summary>
        /// <param name="eventoId">Chave estrangeira que referencia o campo id da tabela evento</param>
        /// <param name="Id">Chave Primaria da tabela lote</param>
        /// <returns>Retorna o lote com o eventoId e Id especificados</returns>
        Task<Lote> GetLoteByIdsAsync(int eventoId,int Id);


    }
}