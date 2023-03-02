using Business;
using Microsoft.AspNetCore.Mvc;
using NavalWar.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Naval_War.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShotController : ControllerBase
	{
		private readonly IGameService gameService;

		public ShotController(IGameService gameService)
		{
			this.gameService = gameService;
		}

		// GET: api/<ShotController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<ShotController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<ShotController>
		[HttpPost]
		public bool Post([FromQuery] int x, int y, int playerId)
		{
			bool returnValue = gameService.Shot(x, y, playerId);
			return returnValue;
		}

		// PUT api/<ShotController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<ShotController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
