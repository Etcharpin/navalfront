using NavalWar.DAL.Models;
using NavalWar.DTO;
using NavalWar.DTO.WebDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public interface IGameService
	{
		List<Box> GetMap(Player player);
		public void PlaceShip(PlayerDTO player, int x, int y, int size, bool orientation);
		public PlayerDTO RemoveShip(PlayerDTO player, int x, int y, int size, bool orientation);
		public void AddPlayer(PlayerDTO player);
		public PlayerDTO GetPlayer(int playerId);
		public void RemovePlayer(int playerId);
		public bool Shot(int x, int y, int playerId);
		public GameArea GetGame(string gameId);
		public void AddGame(string gameId);
		public int GetNbrPlayerPerSession(string gameId);
		public void UpdateSessionService(string gameId, int nbrOfPlayers, int state);
		public GameDAL GetSession(string gameId);
		public void RemoveSession(GameDAL gdal);

		public void updatePlayerService(PlayerDTO player);

    }
}
