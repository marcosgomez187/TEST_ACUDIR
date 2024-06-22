namespace Acudir.Test.Apis.Controllers
{
    using Acudir.Test.Apis.Interfaces;
    using Acudir.Test.Apis.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonasController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        [HttpGet("GetAll")]
        public IEnumerable<Persona> GetAll([FromQuery] string nombre, [FromQuery] string apellido, [FromQuery] int? edad)
        {
            return _personaRepository.GetAll(nombre, apellido, edad);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Persona persona)
        {
            _personaRepository.Add(persona);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Persona persona)
        {
            _personaRepository.Update(persona);
            return Ok();
        }
    }

    
}
