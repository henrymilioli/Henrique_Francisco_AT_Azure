using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Web.Models.Pais;
using Web.Repository.Services;

namespace Web.Controllers
{
    public class PaisController : Controller
    {

        private readonly IApiPais _apiPais;

        public PaisController(IApiPais apiPais)
        {
            _apiPais = apiPais;
        }


        // GET: PaisController
        public async Task<ActionResult> Index()
        {
            var list = await _apiPais.GetAsync();

            return View(list);
        }

        // GET: PaisController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var pais = await _apiPais.GetPaisAsync(id);

            return View(pais);
        }

        // GET: PaisController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaisController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CriarPaisViewModel criarPaisViewModel)
        {
            try
            {
                var urlFoto = UploadFotoPais(criarPaisViewModel.Foto);
                criarPaisViewModel.UrlFoto = urlFoto.Result;

                await _apiPais.PostAsync(criarPaisViewModel);

                return Redirect("/");

            }
            catch
            {
                return View();
            }
        }

        // GET: PaisController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var pais = await _apiPais.GetPaisByIdAsync(id);

            return View(pais);
        }

        // POST: PaisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditarPaisViewModel editarPaisViewModel)
        {
            try
            {
                var urlFoto = UploadFotoPais(editarPaisViewModel.Foto);
                editarPaisViewModel.UrlFoto = urlFoto.Result;

                await _apiPais.PutAsync(id, editarPaisViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaisController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var pais = await _apiPais.GetPaisByIdAsync(id);

            return View(pais);
        }

        // POST: PaisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _apiPais.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<string> UploadFotoPais(IFormFile foto)
        {
            var reader = foto.OpenReadStream();

            var cloundStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=brunoverdanatazure;AccountKey=uW+EZ52dFgNTxS23qdRIN1FwsGXGeAbtxbq6Ef75d1rsOo5thtJIWsySWYTRRU1DFhvOZ/oKznDRztZ1aKxSBw==;EndpointSuffix=core.windows.net");

            var blobClient = cloundStorageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference("fotos-pais");
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            await blob.UploadFromStreamAsync(reader);

            var destinoDaImagemNaNuvem = blob.Uri.ToString();

            return destinoDaImagemNaNuvem;
        }
    }
}
