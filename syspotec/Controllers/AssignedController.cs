using Microsoft.AspNetCore.Mvc;
using Syspotec.Api.Responses;
using Syspotec.Core.DTOs;
using Syspotec.Core.entities;
using Syspotec.Core.Interfaces;

namespace Syspotec.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignedController : ControllerBase
    {
        private readonly IAssignedService assigned;
        public AssignedController(IAssignedService _Assigned)
        {
            assigned = _Assigned;
        }

        [HttpGet]
        public async Task<ActionResult<List<Assigned>>> Get()
        {
            var response = await assigned.Get();
            return Ok(response);
        }

        [HttpGet("{nit}")]
        public async Task<ActionResult<Assigned>> GetById(int nit)
        {
            var response = await assigned.GetByNIT(nit);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AssignedDTO data)
        {
            var nowLocal = DateTime.Now.ToLocalTime();
            var response = await assigned.Post(data, nowLocal);
            string[] opc = { "Registro cargado exitosamente", "No se pudo crear el registro exitosamente" };
            var objResponse = new ApiResponse(opc, response).getResponse();
            if (response) { return Ok(objResponse); } else { return BadRequest(objResponse); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await assigned.Delete(id);
            string[] opc = { "Registro eliminado exitosamente", "No se pudo eliminar el registro exitosamente" };
            var objResponse = new ApiResponse(opc, response).getResponse();
            if (response) { return Ok(objResponse); } else { return BadRequest(objResponse); }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Assigned data)
        {
            var nowLocal = DateTime.Now.ToLocalTime();
            var old_data = await assigned.GetByID(id);

            if (data.NIT == 0){ data.NIT = old_data.NIT; }
            if (data.Id_ticket == 0){ data.Id_ticket = old_data.Id_ticket; }
            if (data.ID_status == 0){ data.ID_status = old_data.ID_status; }
            
            var response = await assigned.Update(id, data, nowLocal);
            string[] opc = { "Registro actualizado exitosamente", "No se pudo actualizar el registro exitosamente" };
            var objResponse = new ApiResponse(opc, response).getResponse();
            if (response) { return Ok(objResponse); } else { return BadRequest(objResponse); }
            
        }
    }
}
