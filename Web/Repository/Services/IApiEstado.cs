using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Estado;

namespace Web.Repository.Services
{
    public interface IApiEstado
    {
        Task<CriarEstadoViewModel> PostAsync(CriarEstadoViewModel criarEstadoViewModel);
        Task<EditarEstadoViewModel> PutAsync(Guid id, EditarEstadoViewModel editarEstadoViewModel);
        Task<List<ListarEstadoViewModel>> GetAsync();
        Task<DetailsEstadoViewModel> GetDetailsEstadoAsync(Guid id);
        Task<ListarEstadoViewModel> GetEstadoByIdAsync(Guid id);
        Task<string> DeleteAsync(Guid id);
    }
}
