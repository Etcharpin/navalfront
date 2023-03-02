using Business;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Naval_War.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatusController : ControllerBase
    {
        private readonly IGameService gameService;

        public PlayerStatusController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        // GET: api/<GameArea>
        //public IActionResult GetMap()
        //{
        //	return Ok(gameService.RecupMap(player));
        //}
        [HttpGet]
        public IActionResult GetPlayer(int playerId)
        {

            return Ok(gameService.GetPlayer(playerId).isready);
        }

        // POST api/<PlayerStatusController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PlayerStatusController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PlayerStatusController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
