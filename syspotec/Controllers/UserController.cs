using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Syspotec.Api.Responses;
using Syspotec.Core.DTOs;
using Syspotec.Core.entities;
using Syspotec.Core.Interfaces;

namespace Syspotec.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService user;
        public UserController(IUserService _user) 
        {
            user = _user;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var response = await user.Get();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var response = await user.GetByID(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User data)
        {
            var response = await user.Post(data);
            string[] opc = { "Registro cargado exitosamente", "No se pudo crear el registro exitosamente" };
            var objResponse = new ApiResponse(opc, response).getResponse();
            if (response) { return Ok(objResponse); } else { return BadRequest(objResponse); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await user.Delete(id);
            string[] opc = { "Registro eliminado exitosamente", "No se pudo eliminar el registro exitosamente" };
            var objResponse = new ApiResponse(opc, response).getResponse();
            if (response) { return Ok(objResponse); } else { return BadRequest(objResponse); }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] User data)
        {
            var old_data = await user.GetByID(id);
            if (data.First_name == null) { data.First_name = old_data.First_name; }
            if (data.Last_name == null) { data.Last_name = old_data.Last_name; }
            if (data.Email == null) { data.Email = old_data.Email; }
            if (data.Password == null) { data.Password = old_data.Password; }

            var response = await user.Update(id, data);
            string[] opc = { "Registro actualizado exitosamente", "No se pudo actualizar el registro exitosamente" };
            var objResponse = new ApiResponse(opc, response).getResponse();
            if (response) { return Ok(objResponse); } else { return BadRequest(objResponse); }
        }

        [HttpPost("auth/")]
        public async Task<ActionResult> Login([FromBody] LoginUserDTO userData)
        {
            var response = await user.GetPWD(userData);
            string[] opc = { "Credenciales correctas", "Credenciales incorrectas" };
   
            if (response) 
            {
                var body = await user.GetByID(userData.nit);
                var User = new
                {
                    data = body,
                    message = response ? opc[0] : opc[1],
                    status = response
                };
                return Ok(User); 
            }
            else 
            {
                var User = new
                {
                    message = response ? opc[0] : opc[1],
                    status = response
                };
                return Unauthorized(User);
            }
        }

    }
}
