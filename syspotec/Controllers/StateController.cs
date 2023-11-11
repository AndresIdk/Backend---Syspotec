using Microsoft.AspNetCore.Mvc;
using Syspotec.Core.entities;
using Syspotec.Core.Interfaces;

namespace Syspotec.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService state;
        public StateController(IStateService _state)
        {
            state = _state;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> Get()
        {
            var response = await state.Get();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetById(int id)
        {
            var response = await state.GetByID(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] State data)
        {
            var response = await state.Post(data);
            if (response) { return Ok("Registro cargado exitosamente"); } else { return BadRequest("No se pudo crear el registro exitosamente"); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await state.Delete(id);
            if (response) { return Ok("Registro eliminado exitosamente"); } else { return BadRequest("No se pudo eliminar el registro exitosamente"); }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] State data)
        {
            var response = await state.Update(id, data);
            if (response) { return Ok("Registro actualizado exitosamente"); } else { return BadRequest("No se pudo actualizar el registro exitosamente"); }
        }
    }
}
