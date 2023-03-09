using Business;
using Microsoft.AspNetCore.Mvc;
using NavalWar.DTO;
using NavalWar.DTO.WebDTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Naval_War.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShipController : ControllerBase
	{
        private readonly IGameService gameService;

        public ShipController(IGameService gameService)
        {
            this.gameService = gameService;
        }


		// GET api/<ValuesController>/5
		[HttpGet]
        public IActionResult GetPlayership(int playerId)
        {
            var ret = gameService.GetPlayer(playerId).shipslist;
            if(ret == null)
            {
                return Ok(string.Empty);
            }
            return Ok(gameService.GetPlayer(playerId).shipslist);
        }

        [HttpPost]
        public IActionResult PostAddShip([FromQuery] int x, int y, int type, bool orientation, int playerId)
        {
            int res = 0;
        	PlayerDTO pdto = gameService.GetPlayer(playerId);
            if(pdto.shipslist == null)
            {
                pdto.shipslist = new List<Ship>();
                res = 0;
            }
        	if (pdto.GameMap.shipnb[type] > 0)
        	{
                res = 1;
        	}
        	else
        	{
        		switch (type)
        		{
        			case 0:
                        Console.WriteLine(pdto.shipslist);
                        pdto.shipslist.Add(new Ship(2));
                        res = gameService.PlaceShip(pdto, x, y, 2, orientation);
                            

                        break;
        			case 1:
                        pdto.shipslist.Add(new Ship(3));
                        res = gameService.PlaceShip(pdto, x, y, 3, orientation);
                            

                        break;
        			case 2:
                        pdto.shipslist.Add(new Ship(4));
                        res = gameService.PlaceShip(pdto, x, y, 4, orientation);
                        

                        break;
        			case 3:
                        pdto.shipslist.Add(new Ship(5));
                        res = gameService.PlaceShip(pdto, x, y, 5, orientation);
                        
                        
                        
        				break;
        		}
        	}

            return Ok(res);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{playerId}")]
        public void Put(int x, int y, bool orientation, int type, int playerId, int newX, int newY, bool newOrientation)
		{
			PlayerDTO pdto = gameService.GetPlayer(playerId);

			switch (type)
			{
				case 0:
					gameService.RemoveShip(pdto, x, y, 2, orientation);
					gameService.PlaceShip(pdto, newX, newY, 2, newOrientation);
					break;
				case 1:
					gameService.RemoveShip(pdto, x, y, 3, orientation);
					gameService.PlaceShip(pdto, newX, newY, 3, newOrientation);
					break;
				case 2:
					gameService.RemoveShip(pdto, x, y, 4, orientation);
					gameService.PlaceShip(pdto, newX, newY, 4, newOrientation);
					break;
				case 3:
					gameService.RemoveShip(pdto, x, y, 5, orientation);
					gameService.PlaceShip(pdto, newX, newY, 5, newOrientation);
					break;
			}
		}

		// DELETE api/<ValuesController>/5
		[HttpDelete]
		public void Delete(int idplayer,int type)
		{
            PlayerDTO playerDTOplayer = gameService.GetPlayer(idplayer);
            playerDTOplayer.shipslist.Remove(playerDTOplayer.shipslist.Find(x => x.ShipType == type));
            gameService.updatePlayerService(playerDTOplayer);

		}
	}
}
