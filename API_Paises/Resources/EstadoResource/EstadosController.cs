using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Paises.Models.Estado;
using AutoMapper;
using Domain.Estado;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Respository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Paises.Resources.EstadoResource
{
    [Route("api/estados")]
    [ApiController]
    public class EstadosController : ControllerBase
    {

        private readonly API_AmigosContext _context;
        private readonly IMapper _mapper;

        public EstadosController(API_AmigosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<EstadosController>
        [HttpGet]
        public ActionResult Get()
        {
            var listaEstado = BuscarEstados();
            
            return Ok(listaEstado);
        }

        // GET api/<EstadosController>/5
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] Guid id)
        {
            var response = BuscarEstadoPorId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // POST api/<EstadosController>
        [HttpPost]
        public ActionResult Post([FromBody] EstadoRequest estadoRequest)
        {
            var error = estadoRequest.Errors();

            if (error.Any())
            {
                return UnprocessableEntity(error);
            }

            var response = CriarEstado(estadoRequest);

            return CreatedAtAction(nameof(Get), new { response.Id }, response);
        }

        // PUT api/<EstadosController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromRoute]Guid id, [FromBody] EstadoRequest estadoRequest)
        {
            var response = BuscarEstadoPorId(id);

            if (response == null)
            {
                return NotFound();
            }

            AlterarEstado(id, estadoRequest);

            return NoContent();
        }

        // DELETE api/<EstadosController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]Guid id)
        {
            var response = BuscarEstadoPorId(id);

            if (response == null)
            {
                return NotFound();
            }

            ExcluirEstado(id);

            return NoContent();
        }

        private IEnumerable<EstadoResponse> BuscarEstados()
        {
            var listaEstados = _context.Estado.ToList();

            return _mapper.Map<IEnumerable<EstadoResponse>>(listaEstados);
        }

        private EstadoResponse CriarEstado(EstadoRequest estadoResquest)
        {
            estadoResquest.Pais = _context.Pais.FirstOrDefault(x => x.Id == estadoResquest.Pais.Id);
            var estado = _mapper.Map<Estado>(estadoResquest);
            estado.Id = Guid.NewGuid();

            _context.Estado.Add(estado);
            _context.SaveChanges();

            return _mapper.Map<EstadoResponse>(estado); ;
        }

        private EstadoResponse BuscarEstadoPorId(Guid id)
        {
            var estado = _context.Estado.Include(x => x.Pais).FirstOrDefault(x => x.Id == id);

            if (estado == null)
                return null;

            EstadoResponse estadoResponse = new EstadoResponse { Id = estado.Id, Name = estado.Name, UrlFoto = estado.UrlFoto, Pais = estado.Pais.Nome };   

            return _mapper.Map<EstadoResponse>(estadoResponse);
        }

        private void AlterarEstado(Guid id, EstadoRequest estadoRequest)
        {
            var estado = _context.Estado.Find(id);

            var aux = estado.Pais;

            estado = _mapper.Map(estadoRequest, estado);

            estado.Pais = aux;

            _context.Estado.Update(estado);
            _context.SaveChanges();
        }

        private void ExcluirEstado(Guid id)
        {
            var estado = _context.Estado.Find(id);

            if (estado == null)
                return;

            _context.Estado.Remove(estado);
            _context.SaveChanges();
        }
    }
}
