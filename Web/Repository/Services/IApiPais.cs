using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Pais;

namespace Web.Repository.Services
{
    public interface IApiPais
    {
        Task<CriarPaisViewModel> PostAsync(CriarPaisViewModel criarPaisViewModel);
        Task<List<ListarPaisViewModel>> GetAsync();
        Task<DetailsPaisViewModel> GetPaisAsync(Guid id);
        Task<ListarPaisViewModel> GetPaisByIdAsync(Guid id);
        Task<EditarPaisViewModel> PutAsync(Guid id, EditarPaisViewModel editarPaisViewModel);
        Task<string> DeleteAsync(Guid id);
    }
}
