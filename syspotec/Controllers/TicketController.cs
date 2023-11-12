using Microsoft.AspNetCore.Mvc;
using Syspotec.Api.Responses;
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
            string[] opc = { "Registro cargado exitosamente", "No se pudo crear el registro exitosamente" };
            var objResponse = new ApiResponse(opc, response).getResponse();
            if (response) { return Ok(objResponse); } else { return BadRequest(objResponse); }
        }

        [HttpDelete("{id}")] 
        public async Task<ActionResult> Delete(int id) 
        {
            var response = await ticket.Delete(id);
            string[] opc = { "Registro eliminado exitosamente", "No se pudo eliminar el registro exitosamente" };
            var objResponse = new ApiResponse(opc, response).getResponse();
            if (response) { return Ok(objResponse); } else { return BadRequest(objResponse); }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Ticket data)
        {
            var old_data = await ticket.GetByID(id);
            if(data.Description == null){ data.Description = old_data.Description; }
            if(data.Priority == null) { data.Priority = old_data.Priority; }

            var response = await ticket.Update(id, data);
            string[] opc = { "Registro actualizado exitosamente", "No se pudo actualizar el registro exitosamente" };
            var objResponse = new ApiResponse(opc, response).getResponse();
            if (response) { return Ok(objResponse); } else { return BadRequest(objResponse); }
        }
    }
}
