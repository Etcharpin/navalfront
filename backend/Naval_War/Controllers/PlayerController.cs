using Business;
using Microsoft.AspNetCore.Mvc;
using NavalWar.DAL;
using NavalWar.DTO;
using NavalWar.DTO.WebDTO;
using System.Web.Http.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Naval_War.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class PlayerController : ControllerBase
	{
		private readonly IGameService gameService;

		public PlayerController(IGameService gameService)
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

            return Ok(gameService.GetPlayer(playerId).GameMap.listBox);
		}

		[HttpPost]
		public void PostAddPlayer([FromQuery] string username, string gameId)
		{
			int nbrPlayers = gameService.GetNbrPlayerPerSession(gameId);

			if (nbrPlayers < 2)
			{ 
				PlayerDTO pdto = new PlayerDTO();
				Map map = new Map(100);

				pdto.Name = username;
				pdto.GameMap = map;
				pdto.GameId = gameId;

				gameService.AddPlayer(pdto);
				gameService.UpdateSessionService(gameId, nbrPlayers + 1,0);
			}
			else
			{
				Console.WriteLine("Trop de joueurs");
			}
		}

		// PUT api/<GameArea>/5
		[HttpPut]
        public void Put(bool ready,int playerId)
        {
            PlayerDTO pdto = gameService.GetPlayer(playerId);
            pdto.isready = ready;
			gameService.updatePlayerService(pdto);
        }

        // DELETE api/<GameArea>/5
        [HttpDelete("{playerId}")]
		public void DeletePlayer(int playerId)
		{
			gameService.RemovePlayer(playerId);
		}
	}
}
