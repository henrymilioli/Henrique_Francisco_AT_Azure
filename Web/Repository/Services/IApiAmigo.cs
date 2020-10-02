using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Amigo;
using static Web.Repository.Services.ApiAmigo;

namespace Web.Repository.Services
{
    public interface IApiAmigo
    {
        Task<CriarAmigoViewModel> PostAsync(CriarAmigoViewModel criarAmigoViewModel);
        Task<EditarAmigoViewModel> PutAsync(Guid id, EditarAmigoViewModel editarAmigoViewModel);
        Task<List<ListarAmigoViewModel>> GetAsync();
        Task<DetailsAmigoViewModel> GetDetailsAmigoAsync(Guid id);
        Task<ListarAmigoViewModel> GetAmigoByIdAsync(Guid id);
        Task<string> DeleteAsync(Guid id);

        Task<CriarAmizadeViewModel> PostAmizadeAsync(Guid id, CriarAmizadeViewModel criarAmizadeViewModel);
        Task<List<ListarAmizadeViewModel>> GetAmizadeAsync(Guid id);
        Task<string> DeleteAmizadeAsync(Guid amizadeId);
        Task<ListarAmizadeViewModel> GetAmizadeByIdAsync(Guid amizadeId);
    }
}
