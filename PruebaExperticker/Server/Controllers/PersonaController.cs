using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PruebaExperticker.Server.DbConext;
using PruebaExperticker.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaExperticker.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly MiDbContext _context;

        public PersonaController(MiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> Get()
        {
            var personas = _context.Personas.ToList();
            return personas;
        }  
        [HttpGet("{DNI}")]
        public async Task<ActionResult<Persona>> Get(string DNI)
        {
            var persona = await _context.Personas.FindAsync(DNI);
            return persona;
        }
        [HttpPost]
        public async Task<ActionResult<Persona>> Post(Persona persona)
        {
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Post", new { DNI = persona.DNI}, persona);
        }        
        [HttpPut]
        public async Task<ActionResult<Persona>> Put(Persona persona)
        {
            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Put", new { DNI = persona.DNI}, persona);
        }        
        
        [HttpDelete("{DNI}")]
        public async Task<ActionResult<Persona>> Delete(string DNI)
        {
            var persona = await _context.Personas.FindAsync(DNI);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();

            return persona;
        }
    }
}
