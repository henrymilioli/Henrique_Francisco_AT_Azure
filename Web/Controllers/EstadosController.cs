using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Web.Models.Estado;
using Web.Repository.Services;

namespace Web.Controllers
{
    public class EstadosController : Controller
    {
        private readonly IApiEstado _apiEstado;
        private readonly IApiPais _apiPais;

        public EstadosController(IApiEstado apiEstado, IApiPais apiPais)
        {
            _apiEstado = apiEstado;
            _apiPais = apiPais;
        }

        // GET: EstadosController
        public async Task<ActionResult> Index()
        {
            var listaEstado = await _apiEstado.GetAsync();

            return View(listaEstado);
        }

        // GET: EstadosController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var estado = await _apiEstado.GetDetailsEstadoAsync(id);

            return View(estado);
        }

        // GET: EstadosController/Create
        public async Task<ActionResult> Create()
        {
            var listaPais = await _apiPais.GetAsync();

            ViewBag.Paises = listaPais;

            return View();
        }

        // POST: EstadosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CriarEstadoViewModel criarEstadoViewModel)
        {
            try
            {
                var urlFoto = UploadFotoEstado(criarEstadoViewModel.Foto);
                criarEstadoViewModel.UrlFoto = urlFoto.Result;

                await _apiEstado.PostAsync(criarEstadoViewModel);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadosController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var estado = await _apiEstado.GetEstadoByIdAsync(id);

            var listaPais = await _apiPais.GetAsync();

            ViewBag.Paises = listaPais;

            return View(estado);
        }

        // POST: EstadosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditarEstadoViewModel editarEstadoViewModel)
        {
            try
            {
                var urlFoto = UploadFotoEstado(editarEstadoViewModel.Foto);
                editarEstadoViewModel.UrlFoto = urlFoto.Result;

                await _apiEstado.PutAsync(id, editarEstadoViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadosController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var estado = await _apiEstado.GetEstadoByIdAsync(id);

            return View(estado);
        }

        // POST: EstadosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _apiEstado.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<string> UploadFotoEstado(IFormFile foto)
        {
            var reader = foto.OpenReadStream();

            var cloundStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=brunoverdanatazure;AccountKey=uW+EZ52dFgNTxS23qdRIN1FwsGXGeAbtxbq6Ef75d1rsOo5thtJIWsySWYTRRU1DFhvOZ/oKznDRztZ1aKxSBw==;EndpointSuffix=core.windows.net");

            var blobClient = cloundStorageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference("fotos-estado");
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            await blob.UploadFromStreamAsync(reader);

            var destinoDaImagemNaNuvem = blob.Uri.ToString();

            return destinoDaImagemNaNuvem;
        }
    }
}
