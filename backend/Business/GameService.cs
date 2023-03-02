using NavalWar.Business;
using NavalWar.DAL;
using NavalWar.DAL.Models;
using NavalWar.DTO;
using NavalWar.DTO.WebDTO;

namespace Business
{
	public class GameService : IGameService
	{
		private readonly IPlayerService playerService;
		private readonly ISessionService sessionService;

		public GameService(IPlayerService ps, ISessionService ss)
		{
			this.playerService = ps;
			this.sessionService = ss;
		}

		public List<Box> GetMap (Player player)
		{
			return player.map.listBox;
		}

		public void PlaceShip(PlayerDTO player, int x, int y, int size, bool orientation)
		{ 
			playerService.PlaceShip(player, x, y, size, orientation); 
		}

		public PlayerDTO RemoveShip(PlayerDTO player, int x, int y, int size, bool orientation)
		{
			PlayerDTO pdto = playerService.DeleteShip(player, x, y, size, orientation);

			return pdto;
		}

		public void AddPlayer(PlayerDTO player)
		{
			PlayerDAL pdal = Extension.PlayerDtoToPlayerDal(player);
			playerService.AddPlayerService(pdal);
		}

		public PlayerDTO GetPlayer(int playerId)
		{
			return playerService.GetPlayerService(playerId);
		}

		public void RemovePlayer(int playerId)
		{
			PlayerDTO pdto = playerService.GetPlayerService(playerId);
			PlayerDAL pdal = Extension.PlayerDtoToPlayerDal(pdto);
			playerService.RemovePlayerService(pdal);
		}

		public bool Shot(int x, int y, int playerId)
		{
			PlayerDTO pdto = playerService.GetPlayerService(playerId);
			if (pdto.GameMap.listBox[x + y * 10].WasHit)
				return false;

			if (pdto.GameMap.listBox[x + y * 10].IsOccupied)
			{
				playerService.Shoot(pdto, x, y);

				return true;
			}

			playerService.Shoot(pdto, x, y);

			return false;
		}

		public GameArea GetGame(string gameId)
		{
			return sessionService.GetPlayersSessionService(gameId);
		}

		public void AddGame(string gameId)
		{
			sessionService.AddSession(gameId);
		}

		public int GetNbrPlayerPerSession(string gameId)
		{
			return sessionService.GetNbrOfPlayers(gameId);
		}

		public void UpdateSessionService(string gameId, int nbrOfPlayers, int state)
		{
			sessionService.UpdateSession(gameId, nbrOfPlayers,state);
		}

		public GameDAL GetSession(string gameId)
		{
			return sessionService.GetSession(gameId);
		}

		public void RemoveSession(GameDAL gdal)
		{
			sessionService.RemoveSession(gdal);
		}


        public GameDAL GetStatus(string id)
		{
			return sessionService.GetSession(id);
		}

        public void updatePlayerService(PlayerDTO player)
		{
			playerService.updatePlayer(player);
		}
    }
}
