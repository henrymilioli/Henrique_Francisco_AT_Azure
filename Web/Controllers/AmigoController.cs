using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Web.Models.Amigo;
using Web.Repository.Services;

namespace Web.Controllers
{
    public class AmigoController : Controller
    {
        private readonly IApiAmigo _apiAmigos;
        private readonly IApiEstado _apiEstado;
        private readonly IApiPais _apiPais;

        public AmigoController(IApiAmigo apiAmigo, IApiEstado apiEstado, IApiPais apiPais)
        {
            _apiAmigos = apiAmigo;
            _apiEstado = apiEstado;
            _apiPais = apiPais;
        }

        // GET: AmigoController
        public async Task<ActionResult> Index()
        {
            var amigos = await _apiAmigos.GetAsync();

            return View(amigos);
        }

        // GET: AmigoController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var amigo = await _apiAmigos.GetDetailsAmigoAsync(id);

            return View(amigo);
        }

        // GET: AmigoController/Create
        public async Task<ActionResult> Create()
        {
            var listaPais = await _apiPais.GetAsync();

            ViewBag.Paises = listaPais;

            var listaEstados = await _apiEstado.GetAsync();

            ViewBag.Estados = listaEstados;

            var listaAmigos = await _apiAmigos.GetAsync();

            ViewBag.Amigos = listaAmigos;

            return View();
        }

        // POST: AmigoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CriarAmigoViewModel criarAmigoViewModel)
        {
            var urlFoto = UploadFotoAmigo(criarAmigoViewModel.Foto);
            criarAmigoViewModel.UrlFoto = urlFoto.Result;

            await _apiAmigos.PostAsync(criarAmigoViewModel);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: AmigoController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var amigo = await _apiAmigos.GetAmigoByIdAsync(id);

            return View(amigo);
        }

        // POST: AmigoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditarAmigoViewModel editarAmigoViewModel)
        {
            var urlFoto = UploadFotoAmigo(editarAmigoViewModel.Foto);
            editarAmigoViewModel.UrlFoto = urlFoto.Result;

            await _apiAmigos.PutAsync(id, editarAmigoViewModel);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AmigoController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var amigo = await _apiAmigos.GetAmigoByIdAsync(id);

            return View(amigo);
        }

        // POST: AmigoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _apiAmigos.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("amigo/{id}/Amizade")]
        public async Task<ActionResult> Amizade()
        {
            var listaAmigos = await _apiAmigos.GetAsync();

            ViewBag.Amigos = listaAmigos;

            return View();
        }

        [HttpPost("amigo/{id}/Amizade")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Amizade(Guid id, CriarAmizadeViewModel criarAmizadeViewModel)
        {
            try
            {
                await _apiAmigos.PostAmizadeAsync(id, criarAmizadeViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("amigo/{id}/DeleteAmizade")]
        public async Task<ActionResult> DeleteAmizade(Guid id)
        {
            var listaAmigos = await _apiAmigos.GetAmizadeAsync(id);

            ViewBag.Amigos = listaAmigos;

            return View();
        }

        [HttpGet("amigo/DeletarAmizade/{amizadeId}")]
        public async Task<ActionResult> DeletarAmizade(Guid amizadeId)
        {
            var amizade = await _apiAmigos.GetAmizadeByIdAsync(amizadeId);

            return View(amizade);
        }

        [HttpPost("amigo/DeletarAmizade/{amizadeId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarAmizade(Guid amizadeId, IFormCollection keyValues)
        {
            try
            {
                await _apiAmigos.DeleteAmizadeAsync(amizadeId);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            } 
        }


        private async Task<string> UploadFotoAmigo(IFormFile foto)
        {
            var reader = foto.OpenReadStream();

            var cloundStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=brunoverdanatazure;AccountKey=uW+EZ52dFgNTxS23qdRIN1FwsGXGeAbtxbq6Ef75d1rsOo5thtJIWsySWYTRRU1DFhvOZ/oKznDRztZ1aKxSBw==;EndpointSuffix=core.windows.net");

            var blobClient = cloundStorageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference("fotos-amigos");
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            await blob.UploadFromStreamAsync(reader);

            var destinoDaImagemNaNuvem = blob.Uri.ToString();

            return destinoDaImagemNaNuvem;
        }
    }
}
