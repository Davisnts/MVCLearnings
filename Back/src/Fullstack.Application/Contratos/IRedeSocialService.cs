using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fullstack.Application.Dtos;

namespace Fullstack.Application.Contratos
{
    public interface IRedeSocialService
    {
        Task<RedeSocialDto[]> UpdateByEvento(int id,RedeSocialDto[]models);
        Task<bool> DeleteByEvento(int eventoId,int redeSocialId);
        Task<RedeSocialDto[]> UpdateByPalestrante(int palestranteId,RedeSocialDto[]models);      
        Task<bool> DeleteByPalestrante(int palestranteId,int redeSocialId);
        Task<RedeSocialDto[]> GetAllByEventoIdAsync(int id);
        Task<RedeSocialDto[]> GetAllByPalestranteIdAsync(int palestranteId);
        Task<RedeSocialDto> GetRedeSocialEventoByIdsAsync(int eventoid,int redeSocialId);
        Task<RedeSocialDto> GetRedeSocialPalestranteByIdsAsync(int palestranteId,int redeSocialId);
    }
}