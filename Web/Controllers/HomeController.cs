using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using Web.Models.Home;
using Web.Repository.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiAmigo _apiAmigo;
        private readonly IApiEstado _apiEstado;
        private readonly IApiPais _apiPais;

        public HomeController(ILogger<HomeController> logger, IApiAmigo apiAmigo, IApiEstado apiEstado, IApiPais apiPais)
        {
            _logger = logger;
            _apiAmigo = apiAmigo;
            _apiEstado = apiEstado;
            _apiPais = apiPais;
        }

        public async Task<IActionResult> Index()
        {
            var index = new IndexViewModel();

            var qtdAmigos = await _apiAmigo.GetAsync();
            var qtdEstado = await _apiEstado.GetAsync();
            var qtdPais = await _apiPais.GetAsync();

            index.QtdAmigos = qtdAmigos.Count;
            index.QtdEstados = qtdEstado.Count;
            index.QtdPais = qtdPais.Count;

            return View(index);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
