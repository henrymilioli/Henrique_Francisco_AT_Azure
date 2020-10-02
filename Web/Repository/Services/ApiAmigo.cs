using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Web.Models.Amigo;

namespace Web.Repository.Services
{
    public partial class ApiAmigo : IApiAmigo
    {
        private readonly HttpClient _httpClient;

        public ApiAmigo()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.BaseAddress = new Uri("http://localhost:59932/");
        }

        public async Task<CriarAmigoViewModel> PostAsync(CriarAmigoViewModel criarAmigoViewModel)
        {
            var criarAmigoViewModelJson = JsonConvert.SerializeObject(criarAmigoViewModel);

            var conteudo = new StringContent(criarAmigoViewModelJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/amigos", conteudo);

            if (response.IsSuccessStatusCode)
            {
                return criarAmigoViewModel;
            }

            else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var listErro = JsonConvert.DeserializeObject<List<string>>(responseContent);

                criarAmigoViewModel.Errors = listErro;

                return criarAmigoViewModel;
            }

            return criarAmigoViewModel;
        }

        public async Task<EditarAmigoViewModel> PutAsync(Guid id, EditarAmigoViewModel editarAmigoViewModel)
        {
            var editarAmigoViewModelJson = JsonConvert.SerializeObject(editarAmigoViewModel);

            var conteudo = new StringContent(editarAmigoViewModelJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/amigos/" + id, conteudo);

            if (response.IsSuccessStatusCode)
            {
                return editarAmigoViewModel;
            }

            else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var listErro = JsonConvert.DeserializeObject<List<string>>(responseContent);

                editarAmigoViewModel.Errors = listErro;

                return editarAmigoViewModel;
            }

            return editarAmigoViewModel;
        }

        public async Task<List<ListarAmigoViewModel>> GetAsync()
        {
            var response = await _httpClient.GetAsync("/api/amigos");

            var responseContent = await response.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<List<ListarAmigoViewModel>>(responseContent);

            return list;
        }

        public async Task<DetailsAmigoViewModel> GetDetailsAmigoAsync(Guid id)
        {
            var response = await _httpClient.GetAsync("/api/amigos/" + id);

            var responseContent = await response.Content.ReadAsStringAsync();

            var amigo = JsonConvert.DeserializeObject<DetailsAmigoViewModel>(responseContent);

            return amigo;
        }

        public async Task<ListarAmigoViewModel> GetAmigoByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync("/api/amigos/" + id);

            var responseContent = await response.Content.ReadAsStringAsync();

            var amigo = JsonConvert.DeserializeObject<ListarAmigoViewModel>(responseContent);

            return amigo;
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync("/api/amigos/" + id);

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }

        public async Task<CriarAmizadeViewModel> PostAmizadeAsync(Guid id, CriarAmizadeViewModel criarAmizadeViewModel)
        {
            var criarAmizadeViewModelJson = JsonConvert.SerializeObject(criarAmizadeViewModel);

            var conteudo = new StringContent(criarAmizadeViewModelJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"http://localhost:59932/api/amigos/{id}/amizades" , conteudo);

            if (response.IsSuccessStatusCode)
            {
                return criarAmizadeViewModel;
            }

            else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var listErro = JsonConvert.DeserializeObject<List<string>>(responseContent);

                criarAmizadeViewModel.Errors = listErro;

                return criarAmizadeViewModel;
            }

            return criarAmizadeViewModel;
        }

        public async Task<List<ListarAmizadeViewModel>> GetAmizadeAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:59932/api/amigos/{id}/deletaramizades");

            var responseContent = await response.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<List<ListarAmizadeViewModel>>(responseContent);

            return list;
        }

        public async Task<string> DeleteAmizadeAsync(Guid amizadeId)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:59932/api/amigos/deletaramizades/{amizadeId}");

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }

        public async Task<ListarAmizadeViewModel> GetAmizadeByIdAsync(Guid amizadeId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:59932/api/amigos/deletaramizades/{amizadeId}");

            var responseContent = await response.Content.ReadAsStringAsync();

            var amizade = JsonConvert.DeserializeObject<ListarAmizadeViewModel>(responseContent);

            return amizade;
        }
    }
}
