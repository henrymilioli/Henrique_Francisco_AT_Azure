using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.Amigo;
using Microsoft.EntityFrameworkCore;
using API_Amigos.Models;
using Respository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Amigos.Resources.AmigoResource
{
    [Route("api/amigos")]
    [ApiController]
    public class AmigosController : ControllerBase
    {

        private readonly API_AmigosContext _context;
        private readonly IMapper _mapper;

        public AmigosController(API_AmigosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<AmigosController>
        [HttpGet]
        public ActionResult Get()
        {
            var listAmigos = BuscarAmigos();

            return Ok(listAmigos);
        }


        // GET api/<AmigosController>/5
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] Guid id)
        {
            var response = BuscarAmigoPorId(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        // POST api/<AmigosController>
        [HttpPost]
        public ActionResult Post([FromBody] AmigoResquest amigoResquest)
        {
            var error = amigoResquest.Errors();

            if (error.Any())
                return UnprocessableEntity(error);

            var response = CriarAmigo(amigoResquest);

            return CreatedAtAction(nameof(Get), new { response.Id }, response);
        }



        // PUT api/<AmigosController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id, [FromBody] AmigoResquest amigoResquest)
        {
            var response = BuscarAmigoPorId(id);

            if (response == null)
                return NotFound();

            AlterarAmigo(id, amigoResquest);

            return NoContent();
        }



        // DELETE api/<AmigosController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            var response = BuscarAmigoPorId(id);

            if (response == null)
                return NotFound();

            ExcluirAmigo(id);

            return NoContent();
        }


        [HttpPost("{id}/Amizades")]
        public ActionResult Post([FromRoute] Guid id, [FromBody] AmizadeRequest amizadeRequest)
        {
            var error = amizadeRequest.Errors();

            if (error.Any())
                return UnprocessableEntity(error);

            var response = CriarAmizade(id, amizadeRequest);

            return CreatedAtAction(nameof(Get), new { response.Id }, response);
        }

        [HttpGet("{id}/deletarAmizades")]
        public ActionResult GetAmizades([FromRoute] Guid id)
        {
            var list = BuscarAmizade(id);

            return Ok(list);
        }

        [HttpGet("deletaramizades/{amizadeid}")]
        public ActionResult GetAmizadesById([FromRoute] Guid amizadeid)
        {
            var response = BuscarAmizadePorId(amizadeid);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpDelete("deletarAmizades/{amizadeid}")]
        public ActionResult DeleteAmizades([FromRoute] Guid amizadeid)
        {
            var response = BuscarAmizadePorId(amizadeid);

            if (response == null)
                return NotFound();

            ExcluirAmizade(amizadeid);

            return NoContent();
        }

        public AmizadeResponse CriarAmizade([FromRoute] Guid id, [FromBody] AmizadeRequest amizadeRequest)
        {
            amizadeRequest.AmigoSolicitacaoId = _context.Amigos.Include(x => x.Pais).Include(x => x.Estado).FirstOrDefault(x => x.Id == id).Id.ToString();
            amizadeRequest.Amigo = _context.Amigos.Include(x => x.Pais).Include(x => x.Estado).FirstOrDefault(x => x.Id == amizadeRequest.Amigo.Id);

            var amizade = _mapper.Map<Amizade>(amizadeRequest);
            amizade.Id = new Guid();

            _context.Amizades.Add(amizade);
            _context.SaveChanges();

            return _mapper.Map<AmizadeResponse>(amizade);
        }

        private IEnumerable<AmigoResponse> BuscarAmigos()
        {
            var listAmigos = _context.Amigos.ToList();

            return _mapper.Map<IEnumerable<AmigoResponse>>(listAmigos);
        }

        private IEnumerable<AmizadeResponse> BuscarAmizade(Guid id)
        {
            var listAmizades = _context.Amizades.Include(x => x.Amigo).Include(x => x.Amigo.Pais).Include(x => x.Amigo.Estado).Where(x => x.AmigoSolicitacaoId == id.ToString()).ToList();

            return _mapper.Map<IEnumerable<AmizadeResponse>>(listAmizades);
        }

        private AmizadeResponse BuscarAmizadePorId(Guid id)
        {
            var amizade = _context.Amizades.Include(x => x.Amigo).Include(x => x.Amigo.Pais).Include(x => x.Amigo.Estado).FirstOrDefault(x => x.Id == id);

            return _mapper.Map<AmizadeResponse>(amizade);
        }

        private AmigoResponseWithAmizades BuscarAmigoPorId(Guid id)
        {
            var amizades = _context.Amizades.Include(x => x.Amigo).Where(x => x.AmigoSolicitacaoId == id.ToString()).ToList();

            var amigo = _context.Amigos.Include(x => x.Pais).Include(x => x.Estado).FirstOrDefault(x => x.Id == id);

            var estado = _context.Estado.Include(x => x.Pais).FirstOrDefault(x => x.Id == amigo.Estado.Id);

            List<string> nomeAmigo = new List<string>();

            foreach(var item in amizades)
            {
                nomeAmigo.Add(item.Amigo.Name);
            }

            amigo.Estado = estado;

            if (amigo == null)
                return null;

            AmigoResponseWithAmizades amigoResponse = new AmigoResponseWithAmizades { Id = amigo.Id, Amigo = nomeAmigo, DtAniversario = amigo.DtAniversario, Estado = amigo.Estado.Name, Email = amigo.Email, Name = amigo.Name, Pais = amigo.Pais.Nome, Sobrenome = amigo.Sobrenome, Telefone = amigo.Telefone, UrlFoto = amigo.UrlFoto };


            return _mapper.Map<AmigoResponseWithAmizades>(amigoResponse);
        }

        private AmigoResponse CriarAmigo(AmigoResquest amigoResquest)
        {
            amigoResquest.Pais = _context.Pais.FirstOrDefault(x => x.Id == amigoResquest.Pais.Id);

            amigoResquest.Estado = _context.Estado.FirstOrDefault(x => x.Id == amigoResquest.Estado.Id);

            var amigo = _mapper.Map<Amigo>(amigoResquest);
            amigo.Id = Guid.NewGuid();

            _context.Amigos.Add(amigo);
            _context.SaveChanges();

            return _mapper.Map<AmigoResponse>(amigo);
        }

        private void AlterarAmigo(Guid id, AmigoResquest amigoResquest)
        {
            var amigo = _context.Amigos.Find(id);

            var auxEstado = amigo.Estado;
            var auxPais = amigo.Pais;

            amigo = _mapper.Map(amigoResquest, amigo);

            amigo.Estado = auxEstado;
            amigo.Pais = auxPais;

            _context.Amigos.Update(amigo);
            _context.SaveChanges();
        }

        private void ExcluirAmigo(Guid id)
        {
            var amigo = _context.Amigos.Find(id);

            if (amigo == null)
                return;

            _context.Amigos.Remove(amigo);
            _context.SaveChanges();
        }

        private void ExcluirAmizade(Guid id)
        {
            var amizade = _context.Amizades.Find(id);

            if (amizade == null)
                return;

            _context.Amizades.Remove(amizade);
            _context.SaveChanges();
        }
    }
}