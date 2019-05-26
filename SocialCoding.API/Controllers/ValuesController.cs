using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialCoding.API.Data;

namespace SocialCoding.API.Controllers {
    //http://localhost:5000/api/Values
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        private readonly SocialCodingContext _context;

        public ValuesController (SocialCodingContext context) => _context = context;

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get () {
            var valores = await _context.Valores.ToListAsync ();
            return Ok (valores);
        }
        // GET api/values/5
        [AllowAnonymous]
        [HttpGet ("{id}")]
        public async Task<IActionResult> Get (int id) {
            var valor = await _context.Valores.FindAsync (id);
            return Ok (valor);
        }
        // POST api/values
        [HttpPost]
        public void Post ([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }
    }
}