using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Paises.Models.Pais;
using AutoMapper;
using Domain.Pais;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Respository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Paises.Resources.PaisResource
{
    [Route("api/paises")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly API_AmigosContext _context;
        private readonly IMapper _mapper;

        public PaisesController(API_AmigosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<PaisesController>
        [HttpGet]
        public ActionResult Get()
        {
            var listPais = BuscarPaises();

            return Ok(listPais);
        }

        // GET api/<PaisesController>/5
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute]Guid id)
        {
            var response = BuscarPaisWithEstadosPorId(id);
            
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // POST api/<PaisesController>
        [HttpPost]
        public ActionResult Post([FromBody] PaisRequest paisRequest)
        {
            var error = paisRequest.Errors();

            if (error.Any())
            {
                return UnprocessableEntity(error);
            }

            var response = CriarPais(paisRequest);

            return CreatedAtAction(nameof(Get), new { response.Id }, response);
        }

        // PUT api/<PaisesController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromRoute]Guid id, [FromBody] PaisRequest paisRequest)
        {
            var response = BuscarPaisPorId(id);

            if (response == null)
            {
                return NotFound();
            }

            AlterarPais(id, paisRequest);

            return NoContent();
        }

        // DELETE api/<PaisesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]Guid id)
        {
            var response = BuscarPaisPorId(id);

            if (response == null)
            {
                return NotFound();
            }

            ExcluirPais(id);

            return NoContent();

        }

        private IEnumerable<PaisResponse> BuscarPaises()
        {
            var listaPaises = _context.Pais.ToList();

            return _mapper.Map<IEnumerable<PaisResponse>>(listaPaises);
        }

        private PaisResponse CriarPais(PaisRequest paisResquest)
        {
            var pais = _mapper.Map<Pais>(paisResquest);
            pais.Id = Guid.NewGuid();

            _context.Pais.Add(pais);
            _context.SaveChanges();

            return _mapper.Map<PaisResponse>(pais); ;
        }

        private PaisResponse BuscarPaisPorId(Guid id)
        {
            var pais = _context.Pais.Find(id);

            if (pais == null)
                return null;

            return _mapper.Map<PaisResponse>(pais);
        }

        private PaisResponseWithEstados BuscarPaisWithEstadosPorId(Guid id)
        {
            var estados = _context.Estado.Include(x => x.Pais).Where(x => x.Pais.Id == id).ToList();

            var pais = _context.Pais.Find(id);

            if (pais == null)
                return null;

            List<string> nomeEstados = new List<string>();

            foreach(var item in estados)
            {
                nomeEstados.Add(item.Name);
            }

            PaisResponseWithEstados paisResponseWithEstados = new PaisResponseWithEstados { Id = pais.Id, Nome = pais.Nome, UrlFoto = pais.UrlFoto, Estados = nomeEstados };

            return _mapper.Map<PaisResponseWithEstados>(paisResponseWithEstados);
        }

        private void AlterarPais(Guid id, PaisRequest paisRequest)
        {
            var pais = _context.Pais.Find(id);

            pais = _mapper.Map(paisRequest, pais);

            _context.Pais.Update(pais);
            _context.SaveChanges();
        }

        private void ExcluirPais(Guid id)
        {
            var pais = _context.Pais.Find(id);

            if (pais == null)
                return;

            _context.Pais.Remove(pais);
            _context.SaveChanges();
        }
    }
}
