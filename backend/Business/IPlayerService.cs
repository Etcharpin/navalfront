using NavalWar.DAL.Models;
using NavalWar.DTO;
using NavalWar.DTO.WebDTO;

namespace NavalWar.Business
{
	public interface IPlayerService
	{
		void PlaceShip(PlayerDTO player, int x, int y, int size, bool orientation);
		public PlayerDTO DeleteShip(PlayerDTO player, int x, int y, int size, bool orientation);
		void AddPlayerService(PlayerDAL player);
		PlayerDTO GetPlayerService(int playerId);

		void RemovePlayerService(PlayerDAL player);
		public void Shoot(PlayerDTO player, int x, int y);

		public void updatePlayer(PlayerDTO player);

    }
}
