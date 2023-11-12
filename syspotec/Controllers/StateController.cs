using Microsoft.AspNetCore.Mvc;
using Syspotec.Api.Responses;
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
            string[] opc = { "Registro cargado exitosamente", "No se pudo crear el registro exitosamente" };
            var objResponse = new ApiResponse(opc, response).getResponse();
            if (response) { return Ok(objResponse); } else { return BadRequest(objResponse); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await state.Delete(id);
            string[] opc = { "Registro eliminado exitosamente", "No se pudo eliminar el registro exitosamente" };
            var objResponse = new ApiResponse(opc, response).getResponse();
            if (response) {  return Ok(objResponse); } else { return  BadRequest(objResponse); }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] State data)
        {
            var response = await state.Update(id, data);
            string[] opc = { "Registro actualizado exitosamente", "No se pudo actualizar el registro exitosamente" };
            var objResponse = new ApiResponse(opc, response).getResponse();
            if (response) { return Ok(objResponse); } else { return BadRequest(objResponse); }
        }
    }
}
