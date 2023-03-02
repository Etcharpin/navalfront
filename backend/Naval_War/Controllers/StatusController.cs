using Business;
using Microsoft.AspNetCore.Mvc;
using NavalWar.DAL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Naval_War.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {

        private readonly IGameService gameService;

        public StatusController(IGameService gameService)
        {
            this.gameService = gameService;
        }
        // GET: api/<NameController>
        [HttpGet]
        public IActionResult GetStatus(string gameid)
        {

            return Ok(gameService.GetSession(gameid).State);
        }

        // GET api/<NameController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NameController>
        [HttpPost]
        public void Post([FromQuery] int value,string id)
        {
            var gs = gameService.GetSession(id);
            gs.State = value;
            gameService.UpdateSessionService(id,gs.NbrPlayer,value);

        }

        // PUT api/<NameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
