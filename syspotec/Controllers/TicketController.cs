using Microsoft.AspNetCore.Mvc;
using Syspotec.Core.entities;
using Syspotec.Core.Interfaces;

namespace Syspotec.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService ticket;
        public TicketController(ITicketService _ticket) 
        {
            ticket = _ticket;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> Get()
        {
            var response = await ticket.Get();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetById(int id)
        {
            var response = await ticket.GetByID(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Ticket data)
        {
            var response = await ticket.Post(data);
            if (response) { return Ok("Registro cargado exitosamente"); } else { return BadRequest("No se pudo crear el registro exitosamente"); }
        }

        [HttpDelete("{id}")] 
        public async Task<ActionResult> Delete(int id) 
        {
            var response = await ticket.Delete(id);
            if (response) { return Ok("Registro eliminado exitosamente"); } else { return BadRequest("No se pudo eliminar el registro exitosamente"); }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Ticket data)
        {
            var old_data = await ticket.GetByID(id);
            if(data.Description == null){ data.Description = old_data.Description; }
            if(data.Priority == null) { data.Priority = old_data.Priority; }

            var response = await ticket.Update(id, data);
            if (response) { return Ok("Registro actualizado exitosamente"); } else { return BadRequest("No se pudo actualizar el registro exitosamente"); }
        }
    }
}
