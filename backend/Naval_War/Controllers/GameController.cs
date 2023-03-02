using Business;
using Microsoft.AspNetCore.Mvc;
using NavalWar.DAL.Models;
using NavalWar.DTO;
using System.Web.Http.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Naval_War.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GameController : ControllerBase
	{
		private readonly IGameService gameService;

		public GameController(IGameService gameService)
		{
			this.gameService = gameService;
		}

		// GET: api/<GameController>
		[HttpGet]
		public IActionResult GetPlayersFromGame(string gameId)
		{
			return Ok(gameService.GetGame(gameId).ListPlayers);
		}

		// POST api/<GameController>
		[HttpPost]
		public void Post([FromQuery] string gameId)
		{
			gameService.AddGame(gameId);
		}

		// PUT api/<GameController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<GameController>/5
		[HttpDelete("{gameId}")]
		public void Delete(string gameId)
		{
			GameDAL gdal = gameService.GetSession(gameId);
			GameArea garea = gameService.GetGame(gameId);

			foreach (Player player in garea.ListPlayers)
			{
				gameService.RemovePlayer(player.Id);
			}

			gameService.RemoveSession(gdal);
		}

		//[HttpGet]
		//[EnableCors("*", "*", "*")]
		//public IActionResult GetGame(int gameId)
		//{
		//	return Ok(gameService.GetSession(gameId));
		//}
		// A tester et voir double Get !!!
	}
}
