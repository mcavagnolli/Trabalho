using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Trabalho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        //public async Task<IActionResult> RecuperarPorId<Guid id>
        //{
        //    //return cinema = await cinemaRepositorio.RecuperarPorIdAsync(id);

        //    //return Ok(cinema)
        //}

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}
